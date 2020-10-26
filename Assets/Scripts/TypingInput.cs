using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Text;
using System;
using UnityEditor;

public class TypingInput : MonoBehaviour
{
    public static TypingInput instance;
    private Keyboard keyboard;
    [System.Serializable]
    public class FinishedLoadingEvent: UnityEvent<string, string> {}
    public FinishedLoadingEvent FinishedLoading;
    public event Action<double> SendBeat = delegate { };
    public event Action<string, string> WordUpdated = delegate { };
    public event Action CharCorrect = delegate { };
    public event Action CharIncorrect = delegate { };
    public event Action<string> WordCompleted = delegate { };
    public WordToType wordToType;
    private StringBuilder currWord;
    private char lastInput;
    
    void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        currWord = new StringBuilder ();
    }
    void Start()
    {
        keyboard = Keyboard.current;
        keyboard.onTextInput += UpdateWord;
        TypingBeatChecker.instance.BeatHit += BeatHitHandler;
    }

    public void GetFirstWord()
    {
        string currWordStr = currWord.ToString();
        FinishedLoading.Invoke(currWordStr, wordToType.GetLeftoverChars(currWordStr));
    }
    void UpdateWord(char c) 
    {
        if (c == '\n' || c == '\r')
        {
            return;
        }
        else
        {
            // if backspace, remove last letter
            if (c == '\b' && currWord.Length > 0) 
            {
                currWord.Remove(currWord.Length - 1, 1); 
                string currWordStr = currWord.ToString();
                WordUpdated(currWordStr, wordToType.GetLeftoverChars(currWordStr));
            }
            else
            {
                lastInput = c;
                SendBeat(Conductor.instance.songTime);
            }
        }
    }

    private void BeatHitHandler()
    {
        currWord.Append(lastInput);
        string currWordStr = currWord.ToString();
        if(wordToType.IsValidString(currWordStr))
        {
            WordUpdated(currWordStr, wordToType.GetLeftoverChars(currWordStr));
            CameraController.instance.Zoom();
            CharCorrect();
        }
        else
        {
            currWord.Remove(currWord.Length - 1, 1); 
            CharIncorrect();
        }
        if(wordToType.CheckCompleteString(currWordStr))
        {
            WordCompleted(currWordStr);
            currWord.Clear();
            currWordStr = currWord.ToString();
            WordUpdated(currWordStr, wordToType.GetLeftoverChars(currWordStr));            
        }
    }

    void OnDestroy()
    {
        keyboard.onTextInput -= UpdateWord;
        TypingBeatChecker.instance.BeatHit -= BeatHitHandler;
    }

}
