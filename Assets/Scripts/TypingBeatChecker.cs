using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class TypingBeatChecker : MonoBehaviour
{
    public static TypingBeatChecker instance;
    public event Action BeatHit = delegate { };
    public event Action BeatMiss = delegate { };
    public double fudgeAmount;
    private double beatReceivedTime, typingReceivedTime;
    private bool beatReceived, typingReceived;


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
    }
    void Start()
    {
        Conductor.instance.SendTimedBeat += ConductorBeatHandler;
        Conductor.instance.SendTimeout += TimeoutHandler;
        TypingInput.instance.SendBeat += TypingBeatHandler;
        
    }

    private void ConductorBeatHandler(double songTime)
    {
        beatReceivedTime = songTime;
        beatReceived = true;
        // Debug.Log("Beat received at " + songTime);        
        if(typingReceived)
        {
            CheckHit();
        }
        // Debug.Log("Beat received, diff: " + (typingReceivedTime - beatReceivedTime));
        // if(Math.Abs(typingReceivedTime - beatReceivedTime) < fudgeAmount)
        // {
        //     Debug.Log("Hit");
        //     BeatHit();
        // }
    }

    private void TypingBeatHandler(double songTime)
    {
        typingReceivedTime = songTime;
        typingReceived = true;
        // Debug.Log("Typing received at " + songTime);
        if(beatReceived)
        {
            CheckHit();
        }
        // Debug.Log("Typing received, diff: " + (typingReceivedTime - beatReceivedTime));
        // if(Math.Abs(typingReceivedTime - beatReceivedTime) < fudgeAmount)
        // {
        //     Debug.Log("Hit");
        //     BeatHit();
        // }
        // else
        // {
        //     Debug.Log("Miss");
        //     BeatMiss();
        // }
    }

    private void TimeoutHandler(double songTime)
    {
        // Debug.Log("Timeout at " + songTime);
        beatReceived = false;
    }

    private void CheckHit()
    {
        Debug.Log("Diff: " + (typingReceivedTime - beatReceivedTime));
        if(Math.Abs(typingReceivedTime - beatReceivedTime) < fudgeAmount)
        {
            BeatHit();
        }
        else
        {
            BeatMiss();
        }
        beatReceived = false;
        typingReceived = false;
    }
    void OnDestroy()
    {
        Conductor.instance.SendTimedBeat -= ConductorBeatHandler;
        TypingInput.instance.SendBeat -= TypingBeatHandler;
    }
}
