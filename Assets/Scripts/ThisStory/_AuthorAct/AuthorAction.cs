using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthorAction : MonoBehaviour, IAction
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
        PlayTestAnimation();
    }

    void IAction.ContinueAction() {
        _onFinish();
    }
    //---------------------------------------------------------------------------------------
    #endregion

    public Image Panel;
    public float AnimationTime;

    private int? _animationId;

    void PlayTestAnimation()
    {
        if (_animationId.HasValue)
        {
            LeanTween.cancel(_animationId.Value);
            _animationId = null;
        }

        if (AnimationTime == 0) {
            AnimationTime = 1;
        }

        Panel.color = GameHiddenOptions.Instance.BlackColor;
        _animationId = LeanTween.color(
            Panel.GetComponent<RectTransform>(),
            GameHiddenOptions.Instance.RedColor,
            AnimationTime
        ).id;
        LeanTween.descr(_animationId.Value).setEase(LeanTweenType.linear);
        // LeanTween.descr(_animationId.Value).setOnUpdate((float val) =>
        // {
        //     Debug.Log(val);
        // });
        LeanTween.descr(_animationId.Value).setOnComplete(OnTestAnimationEnd);
    }

    void OnTestAnimationEnd()
    {
        LeanTween.cancel(_animationId.Value);
        _animationId = null;

        ActController.Instance.RequestContinue();
    }
}
