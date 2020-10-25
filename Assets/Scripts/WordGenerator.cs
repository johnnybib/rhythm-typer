using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.IO;

public class WordGenerator : MonoBehaviour
{
    private List<string> words;
    private StreamReader reader;
    public TextAsset dictionary;
    // public int timeSignature;

    void Awake()
    {
        words = new List<string>(dictionary.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.RemoveEmptyEntries));
    }

    void Start()
    {
        // timeSignature = Conductor.instance.timeSignature;
    }
    
    public string GetWord()
    {
        int idx = 0;
        // for(int i = 0; i < 1000; i++)
        // {
        idx = Random.Range(0, words.Count);
        //     if(words[idx].Length % timeSignature  == 0)
        //         break;
        // }
        return words[idx];
    }
}