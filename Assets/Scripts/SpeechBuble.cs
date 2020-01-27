using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public delegate void SayLineFinished();

public class SpeechBuble : MonoBehaviour
{
    public TextMeshProUGUI DialogText;
    private Image _image;
    public bool IsWhite;
    public float Slowdown;
    public bool IsInUse;
    public int Index;
    private SayLineFinished _onSayLineFinished;
    private int? _animationId;
    private IEnumerator _sayLineCo;

    public void InitLine(string text, int index, SayLineFinished onSayLineFinished)
    {
        Slowdown = 1.0f;
        DialogText.text = text;
        Index = index;
        _onSayLineFinished = onSayLineFinished;

        if (_image == null)
        {
            _image = GetComponent<Image>();
        }
    }

    public void SayLine()
    {
        if (_sayLineCo != null) {
            StopCoroutine(_sayLineCo);
            _sayLineCo = null;
        }
        _sayLineCo = SayLineCo();
        StartCoroutine(_sayLineCo);
    }

    private IEnumerator SayLineCo()
    {
        yield return new WaitForSeconds(GameHiddenOptions.Instance.TimeBeforeReadingLine);

        SayItService.Instance().WhiteColor = IsWhite;
        SayItService.Instance().SayLine(DialogText, () =>
        {
            IsInUse = false;
            _onSayLineFinished();
        }, Slowdown);
    }

    public void SetActive()
    {
        _image.color = GameHiddenOptions.Instance.WhiteTransparentColor;
        gameObject.SetActive(true);
        if (_animationId.HasValue)
        {
            LeanTween.cancel(_animationId.Value);
            _animationId = null;
        }
        _animationId = LeanTween.alpha(
            _image.GetComponent<RectTransform>(),
            1f,
            GameHiddenOptions.Instance.TimeToLoadDialogueBoxes
        ).id;
        LeanTween.descr(_animationId.Value).setEase(LeanTweenType.linear);
    }


}
