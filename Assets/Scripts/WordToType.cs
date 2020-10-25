using UnityEngine;
using TMPro;
using System;
public class WordToType : MonoBehaviour
{
    public event Action FinishedLoading = delegate { };
    public WordGenerator wordGen;
    public TextMeshProUGUI nextWordText;
    public string nextWord;
    public string currentWord;
    void Awake()
    {
        //OK to put event listener in awake here since we know that we have the reference to wordGen already
        wordGen.FinishedLoading += GenerateFirstWord;
    }
    void GenerateFirstWord()
    {
        nextWord = wordGen.GetWord();
        GetNewWord();
        FinishedLoading();
    }
    public bool IsValidString(string inputString)
    {
        return currentWord.IndexOf(inputString) == 0;
    }

    public string GetLeftoverChars(string inputString)
    {
        return currentWord.Substring(inputString.Length);
    }
    public bool CheckCompleteString(string inputString)
    {
        if(currentWord == inputString)
        {
            GetNewWord();
            return true;
        }
        else
            return false;
    }

    private void GetNewWord()
    {
        currentWord = nextWord;
        nextWord = wordGen.GetWord();
        nextWordText.text = nextWord;
    }
}