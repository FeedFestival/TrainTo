using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    private static DialogueController _instance;
    public static DialogueController Instance { get { return _instance; } }
    void Awake()
    {
        _instance = this;
        foreach (Transform child in DialogueBoxesParent)
        {
            GameObject.Destroy(child.gameObject);
        }
        InitDialoguePrefabs();
    }

    public Transform DialogueBoxesParent;

    [Header("DialoguePrefabs")]
    public GameObject dbs;
    private Dictionary<string, GameObject> _dialoguePrefabs;
    private List<SpeechBuble> _speechBublePool;
    public Text CurrentDialogueBox;
    private Line _currentLine;
    private int _dialogBoxIndex;
    private int _index;
    private int _sentenceIndex;
    private IEnumerator _startSayLine;
    private SayLineFinished _onSayLinesFinished;

    public void Show(Line line = null, bool initial = false, SayLineFinished onSayLinesFinished = null)
    {
        if (initial)
        {
            Reset(line, onSayLinesFinished);
            foreach (var dialogBox in line.DialogBoxes)
            {
                CreateDialogueBoxes(dialogBox);
                _sentenceIndex++;
            }
        }
    }

    public void Hide()
    {
        Reset();
        foreach (var speechBuble in _speechBublePool)
        {
            speechBuble.SetDisabled();
        }
    }

    private void Reset(Line line = null, SayLineFinished onSayLinesFinished = null)
    {
        _currentLine = line;
        _dialogBoxIndex = 0;
        _onSayLinesFinished = onSayLinesFinished;
        _sentenceIndex = 0;
    }

    internal void SayLines()
    {
        if (_startSayLine != null)
        {
            StopCoroutine(_startSayLine);
            _startSayLine = null;
        }
        _startSayLine = StartSayLine();
        StartCoroutine(_startSayLine);
    }

    IEnumerator StartSayLine()
    {
        yield return new WaitForSeconds(GameHiddenOptions.Instance.TimeBeforeSayLine);
        var speechBuble = GetDialoguePrefab();
        speechBuble.SayLine();
    }

    void SayLineFinished()
    {
        _dialogBoxIndex++;
        if (_dialogBoxIndex >= _currentLine.DialogBoxes.Length)
        {
            _currentLine = null;
            if (_onSayLinesFinished != null)
            {
                _onSayLinesFinished();
            }
            return;
        }
        SayLines();
    }

    private void InitDialoguePrefabs()
    {
        _dialoguePrefabs = new Dictionary<string, GameObject>()
        {
            { "dbs", dbs }
        };
    }

    private SpeechBuble GetDialoguePrefab()
    {
        DialogBox dialogBox = _currentLine.DialogBoxes[_dialogBoxIndex];
        return _speechBublePool.FirstOrDefault(s =>
            s.gameObject.name == dialogBox.DialogBoxType.ToString()
            && s.gameObject.activeSelf == true
            && s.Index == dialogBox.Index
            );
    }

    void CreateDialogueBoxes(DialogBox dialogBox)
    {
        GameObject go = null;
        string name = dialogBox.DialogBoxType.ToString();
        if (_dialoguePrefabs.ContainsKey(name))
        {
            if (_speechBublePool == null)
            {
                _speechBublePool = new List<SpeechBuble>();
            }
            var existingGo = _speechBublePool.FirstOrDefault(s => s.gameObject.name == name && s.gameObject.activeSelf == false);
            if (existingGo != null)
            {
                go = existingGo.gameObject;
            }
            else
            {
                go = GameHiddenOptions.Instance.GetAnInstantiated(_dialoguePrefabs[name]);
                go.SetActive(false);
                _speechBublePool.Add(go.GetComponent<SpeechBuble>());
            }
        }

        go.name = name;
        go.transform.SetParent(DialogueBoxesParent, false);
        var rT = go.GetComponent<RectTransform>();
        rT.anchoredPosition3D = new Vector3(dialogBox.posX, dialogBox.posY, dialogBox.posZ);
        rT.sizeDelta = new Vector2(dialogBox.width, dialogBox.height);
        rT.anchorMin = new Vector2(dialogBox.anchorsMinX, dialogBox.anchorsMinY);
        rT.anchorMax = new Vector2(dialogBox.anchorsMaxX, dialogBox.anchorsMaxY);
        rT.pivot = new Vector2(dialogBox.pivotX, dialogBox.pivotY);
        rT.eulerAngles = new Vector3(dialogBox.rotationX, dialogBox.rotationY, dialogBox.rotationZ);
        rT.localScale = new Vector3(dialogBox.sizeX, dialogBox.sizeY, dialogBox.sizeZ);

        var crT = rT.GetChild(0).GetComponent<RectTransform>();
        crT.sizeDelta = new Vector2(dialogBox.childWidth, dialogBox.childHeight);

        go.GetComponent<SpeechBuble>().InitLine(_currentLine.Sentences[_sentenceIndex], dialogBox.Index, SayLineFinished);
        go.GetComponent<SpeechBuble>().SetActive();
    }
}
