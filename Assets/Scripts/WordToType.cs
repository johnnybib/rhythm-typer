using UnityEngine;
using TMPro;
public class WordToType : MonoBehaviour
{
    public WordGenerator wordGen;
    public TextMeshProUGUI nextWordText;
    public string nextWord;
    public string currentWord;
    void Awake()
    {
        nextWord = wordGen.GetWord();
        GetNewWord();
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