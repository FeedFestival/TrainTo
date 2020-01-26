using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelloAct_Action_1 : MonoBehaviour, IAction
{
    #region IAction
    //---------------------------------------------------------------------------------------
    private FinishEvent _onFinish;

    void IAction.InitAction(FinishEvent onFinish)
    {
        _onFinish = onFinish;
    }

    void IAction.Do()
    {
        PlayConversation();
    }

    void IAction.ContinueAction() {
        _onFinish();
    }
    //---------------------------------------------------------------------------------------
    #endregion

    public Image Panel;

    private List<Line> _lines;

    void PlayConversation()
    {
        foreach(var line in _lines) {
            // instantiate SpeechBox with properties from DialogBox
            
        }
    }

    void OnTestAnimationEnd()
    {
        

        ActController.Instance.RequestContinue();
    }
}
