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
rotationX = 0f, rotationY = 0f, rotationZ = 0f, childWidth = 70.9f, childHeight = 28.3f
                    },
                    new DialogBox() {
                        DialogBoxType = DialogBoxType.dbs,
posX = 311.68f, posY = 417.9f, posZ = 0f, width = 143.4f, height = 156f, sizeX = 1f, sizeY = 1f, sizeZ = 1f,
anchorsMinX = 0f, anchorsMinY = 0f, anchorsMaxX = 0f, anchorsMaxY = 0f, pivotX = 0f, pivotY = 0f, 
rotationX = 0f, rotationY = 0f, rotationZ = 0f, childWidth = 86.3f, childHeight = 58.3f
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
