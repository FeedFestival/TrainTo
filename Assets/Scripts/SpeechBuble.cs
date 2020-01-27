using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBuble : MonoBehaviour
{
    // private string Text = "Ana are mere. \nSi I le da lui Ionel care ionel le ia si, \nsi le baga in cur.";
    public string Text;
    public TextMeshProUGUI DialogText;
    public bool IsWhite;
    public float Slowdown;
    public bool IsInUse;
    public int Index;

    public delegate void SayLineFinished();
    private SayLineFinished _onSayLineFinished;

    public void InitLine(string text, int index, SayLineFinished onSayLineFinished)
    {
        Slowdown = 1.0f;
        Text = text;
        DialogText.text = Text;
        Index = index;
        _onSayLineFinished = onSayLineFinished;
    }

    public void SayLine()
    {
        SayItService.Instance().WhiteColor = IsWhite;
        SayItService.Instance().SayLine(DialogText, Text, () =>
        {
            IsInUse = false;
            _onSayLineFinished();
        }, Slowdown);
    }
}
