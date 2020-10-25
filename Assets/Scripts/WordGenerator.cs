using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using System;
public class WordGenerator : MonoBehaviour
{
    public event Action FinishedLoading = delegate { };
    private List<string> words;
    private TextAsset dictionary;
    // public int timeSignature;

    void Start()
    {
        dictionary = Resources.Load("dictionary", typeof(TextAsset)) as TextAsset;
        words = new List<string>(dictionary.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.RemoveEmptyEntries));
        FinishedLoading();
    }

    
    public string GetWord()
    {
        int idx = 0;
        // for(int i = 0; i < 1000; i++)
        // {
        idx = UnityEngine.Random.Range(0, words.Count);
        //     if(words[idx].Length % timeSignature  == 0)
        //         break;
        // }
        return words[idx];
    }

}