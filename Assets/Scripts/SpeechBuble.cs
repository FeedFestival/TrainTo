using UnityEngine;
using UnityEngine.UI;

public class SpeechBuble : MonoBehaviour
{
    // private string Text = "Ana are mere. \nSi I le da lui Ionel care ionel le ia si, \nsi le baga in cur.";
    public string Text;
    public Text DialogText;
    public bool IsWhite;
    public float Slowdown;

    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(Text))
        {
            Text = DialogText.text;
        }
        if (Slowdown == 0) {
            Slowdown = 1.0f;
        }

        SayLine();
    }

    void SayLine()
    {
        SayItService.Instance().WhiteColor = IsWhite;
        SayItService.Instance().SayLine(DialogText, Text, () =>
        {
            Debug.Log("S-a terminat!");
        }, Slowdown);
    }
}
