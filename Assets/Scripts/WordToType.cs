using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
public class WordToType : MonoBehaviour
{
    public UnityEvent FinishedLoading;
    public WordGenerator wordGen;
    public TextMeshProUGUI nextWordText;
    public string nextWord;
    public string currentWord;
    public void GenerateFirstWord()
    {
        nextWord = wordGen.GetWord();
        GetNewWord();
        FinishedLoading.Invoke();
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