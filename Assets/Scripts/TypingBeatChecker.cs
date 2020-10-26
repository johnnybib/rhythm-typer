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
    private double beatReceivedTime;


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
        TypingInput.instance.SendBeat += TypingBeatHandler;
    }

    private void ConductorBeatHandler(double songTime)
    {
        beatReceivedTime = songTime;
    }

    private void TypingBeatHandler(double songTime)
    {
        if(Math.Abs(songTime - beatReceivedTime) < fudgeAmount)
        {
            BeatHit();
        }
        else
        {
            BeatMiss();
        }
    }

    void OnDestroy()
    {
        Conductor.instance.SendTimedBeat -= ConductorBeatHandler;
        TypingInput.instance.SendBeat -= TypingBeatHandler;
    }
}
