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
        InitLines();
    }

    void IAction.Do()
    {
        PlayConversation();
    }

    void IAction.ContinueAction()
    {
        _onFinish();
    }
    //---------------------------------------------------------------------------------------
    #endregion

    public Image Panel;

    private List<Line> _lines;

    void PlayConversation()
    {
        StartCoroutine(WaitABit());

        // foreach (var line in _lines)
        // {
        //     // instantiate SpeechBox with properties from DialogBox

        // }
    }

    IEnumerator WaitABit() {
        yield return new WaitForSeconds(1f);
        DialogueController.Instance.SayLine(_lines[0], true);
    }

    private void InitLines()
    {
        _lines = new List<Line>()
        {
            new Line() {
                DialogBoxes = new DialogBox[2] {
                    new DialogBox() {
                        DialogBoxType = DialogBoxType.dbs,
posX = 381f, posY = 377.55f, posZ = 0f, width = 115.7f, height = 105.3f, sizeX = 1f, sizeY = 1f, sizeZ = 1f,
anchorsMinX = 0f, anchorsMinY = 0f, anchorsMaxX = 0f, anchorsMaxY = 0f, pivotX = 0f, pivotY = 0f, 
rotationX = 0f, rotationY = 0f, rotationZ = 0f, childWidth = 72.12f, childHeight = 50f
                    },
                    new DialogBox() {
                        DialogBoxType = DialogBoxType.dbs,
posX = 303f, posY = 428f, posZ = 0f, width = 235.8f, height = 201.9f, sizeX = 1f, sizeY = 1f, sizeZ = 1f,
anchorsMinX = 0f, anchorsMinY = 0f, anchorsMaxX = 0f, anchorsMaxY = 0f, pivotX = 0f, pivotY = 0f, 
rotationX = 0f, rotationY = 0f, rotationZ = 0f, childWidth = 157.7f, childHeight = 85.7f
                    }
                },
                Sentences = new string[2] {
                    "Hello",
                    "Where \nare you \nheading?"
                }
            }
        };
    }
}
