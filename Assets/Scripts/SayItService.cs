using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class SayItService : MonoBehaviour
{
    private static SayItService _this;
    public static SayItService Instance()
    {
        return _this;
    }
    void Awake()
    {
        _this = this;
    }

    public bool WhiteColor;
    public delegate void OnLineEnd();
    private OnLineEnd _onLineEnd;
    private Text _dialogText;
    private int _dialogTextFontSize;
    private string _maxfSize;
    readonly char[] delimeters = new char[3] { ' ', '.', ',' };
    private float _timePerWordRatio = 0.08f;
    private float _timePerWord;
    private string[] _words;
    private int _wordCount;

    private readonly Color32 _firstColorWhite = new Color32(81, 81, 81, 255);
    private readonly Color32 _firstColorBlack = new Color32(147, 147, 147, 147);
    private string _hexColorWhite = "DBDBDB";
    private string _hexColorBlack = "#343434";

    public void SetLineOptions(Text dialogText, OnLineEnd onLineEnd)
    {
        _dialogText = dialogText;
        _dialogTextFontSize = _dialogText.fontSize;
        _maxfSize = "size=" + (_dialogTextFontSize + 1);
        _onLineEnd = onLineEnd;
    }

    public void SayLine(Text dialogText, string line, OnLineEnd onLineEnd, float slowdown = 1) {
        SetLineOptions(dialogText, onLineEnd);
        SayLine(line, slowdown);
    }

    public void SayLine(string line, float slowdown = 1)
    {
        line = FixLine(line);
        _words = SplitAndKeepSeparators(line, delimeters, StringSplitOptions.RemoveEmptyEntries);
        _words = _words.Where(w => w != " " && w != "\n").ToArray();
        _wordCount = _words.Length;

        _timePerWord = _timePerWordRatio * slowdown;

        _dialogText.color = WhiteColor == true ? _firstColorWhite : _firstColorBlack;
        _dialogText.text = line;

        StartCoroutine(UpdateTextByWord());
        // Test();
    }

    // public void Test()
    public IEnumerator UpdateTextByWord()
    {
        string newWord = "";
        int textIndex = 0;
        StringBuilder stringBuilder;
        bool firstWord = true;
        foreach (var word in _words)
        {
            var space = (firstWord ? "" : " ");
            if (word.Length == 1 && delimeters.Any(d => d == word[0]))
            {
                space = "";
            }
            string bWord = "<b></b>";
            newWord = "<color=" + (WhiteColor == true ? _hexColorWhite : _hexColorBlack) + ">"
                + "<" + _maxfSize + ">"
                + "<b>" + space + word
                + "</b></size></color>";
            int wordLength = (space + word).Length;

            if (firstWord)
            {
                textIndex = 0;
                firstWord = false;
            }

            stringBuilder = new StringBuilder(_dialogText.text);
            stringBuilder.Remove(textIndex, wordLength);
            stringBuilder.Insert(textIndex, newWord);

            textIndex += newWord.Length - bWord.Length;

            float time = _timePerWord;
            if (word.Contains(".") || word.Contains(","))
            {
                time = _timePerWord + 0.20f;
                _dialogText.text = stringBuilder.ToString();
            }
            else
            {
                yield return new WaitForSeconds((wordLength * 0.05f));
                _dialogText.text = stringBuilder.ToString();
            }

            yield return new WaitForSeconds(time);
            _dialogText.text = _dialogText.text.Replace("" + _maxfSize + "", "size=" + _dialogTextFontSize);
            _dialogText.text = _dialogText.text.Replace("<b>", "");
            _dialogText.text = _dialogText.text.Replace("</b>", "");
        }
        _onLineEnd();
    }

    private string FixLine(string line) {
        line = line.Replace("\\n", "\n");
        line = line.Replace("\n ", "\n");
        return line;
    }

    private string[] SplitAndKeepSeparators(string value, char[] separators, StringSplitOptions splitOptions)
    {
        List<string> splitValues = new List<string>();
        int itemStart = 0;
        for (int pos = 0; pos < value.Length; pos++)
        {
            for (int sepIndex = 0; sepIndex < separators.Length; sepIndex++)
            {
                if (separators[sepIndex] == value[pos])
                {
                    // add the section of string before the separator 
                    // (unless its empty and we are discarding empty sections)
                    if (itemStart != pos || splitOptions == System.StringSplitOptions.None)
                    {
                        splitValues.Add(value.Substring(itemStart, pos - itemStart));
                    }
                    itemStart = pos + 1;

                    // add the separator
                    splitValues.Add(separators[sepIndex].ToString());
                    break;
                }
            }
        }

        // add anything after the final separator 
        // (unless its empty and we are discarding empty sections)
        if (itemStart != value.Length || splitOptions == System.StringSplitOptions.None)
        {
            splitValues.Add(value.Substring(itemStart, value.Length - itemStart));
        }

        return splitValues.ToArray();
    }
}
