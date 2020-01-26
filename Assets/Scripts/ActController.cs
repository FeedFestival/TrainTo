using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActController : MonoBehaviour
{
    private static ActController _instance;
    public static ActController Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
    }

    public string[] ActDefs;
    public List<IAct> Acts;

    IAct CurrentAct;

    private int ActIndex;
    private int ActsLength;
    public string StartAtAct;
    public Overlay Overlay;
    public bool RequestedContinueAction = false;
    public GameObject TapRequestGo;

    // Start is called before the first frame update
    void Start()
    {
        ActDefs = new string[2] {
            "AuthorAct",
            "HelloAct_1"
        };
        Acts = new List<IAct>();
        Overlay.gameObject.SetActive(true);
        Overlay.SetTransition(OverlayTransition.Transparent);
        TapRequestGo.SetActive(false);

        StartStory();
    }

    void StartStory()
    {
        ActIndex = 0;
        StartAct(StartAtAct);
    }

    private void OnActEnd()
    {
        ActIndex++;

        if (ActIndex >= ActDefs.Length)
        {
            return;
        }

        Overlay.Transition(OverlayTransition.Complete, 1f, () => { StartAct(); });
    }

    private void StartAct(string startAtAct = null)
    {
        if (CurrentAct != null)
        {
            CurrentAct.GameObject.SetActive(false);
        }
        if (startAtAct != null)
        {
            CurrentAct = PrefabManager.Instance().GetActObject(startAtAct);
        }
        else
        {
            CurrentAct = PrefabManager.Instance().GetActObject(ActDefs[ActIndex]);
        }

        Acts.Add(CurrentAct);
        CurrentAct.InitAct(OnActEnd);
        ContinueStory();
    }

    public void ContinueStory()
    {
        CurrentAct.ContinueAct();
    }

    public void ContinueStoryAction()
    {
        CurrentAct.ContinueCurrentAction();
    }

    public void RequestContinue()
    {
        RequestedContinueAction = true;
        TapRequestGo.SetActive(true);
    }

    public void OnAdvanceTap()
    {
        if (RequestedContinueAction)
        {
            RequestedContinueAction = false;
            TapRequestGo.SetActive(false);
            ContinueStoryAction();
            return;
        }
        ContinueStory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnAdvanceTap();
        }
    }
}
