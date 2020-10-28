using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class TypingBeatChecker : MonoBehaviour
{
    public static TypingBeatChecker instance;
    // public event Action BeatHit = delegate { };
    // public event Action BeatMiss = delegate { };
    [Tooltip("0: must be exactly on beat. 1: can be anywhere from exactly on beat to exactly half a beat off (worst precision possible")]
    [Range(0, 1)]
    public double minPrecision;
    private double beatReceivedTime, typingReceivedTime;
    private bool beatReceived, typingReceived;


    // void Awake()
    // {
    //     if(instance)
    //     {
    //         Destroy(gameObject);
    //     }
    //     else
    //     {
    //         instance = this;
    //     }
    // }
    // void Start()
    // {
    //     Conductor.instance.SendTimedBeat += ConductorBeatHandler;
    //     Conductor.instance.SendTimeout += TimeoutHandler;
    //     TypingInput.instance.SendBeat += TypingBeatHandler;
    // }

    // private void ConductorBeatHandler(double songTime)
    // {
    //     beatReceivedTime = songTime;
    //     beatReceived = true;
    //     if(typingReceived)
    //     {
    //         // BeatHit();
    //         // beatReceived = false;
    //         // typingReceived = false; 
    //         CheckHit();
    //     }
    // }

    // private void TypingBeatHandler(double songTime)
    // {
    //     typingReceivedTime = songTime;
    //     typingReceived = true;
    //     if(beatReceived)
    //     {
    //         // BeatHit();
    //         // beatReceived = false;
    //         // typingReceived = false; 
    //         CheckHit();
    //     }
    // }

    // private void TimeoutHandler(double songTime)
    // {
    //     beatReceived = false;
    //     typingReceived = false;
    // }

    // private void CheckHit()
    // {
    //     Debug.Log("Diff: " + (typingReceivedTime - beatReceivedTime) + " compare to " + Conductor.instance.GetSecPerBeat() * 0.5f * minPrecision);
    //     //Precision from player should be within half a beat times our min precision (0 to 1)
    //     if(Math.Abs(typingReceivedTime - beatReceivedTime) < Conductor.instance.GetSecPerBeat() * 0.5f * minPrecision)
    //     {
    //         BeatHit();
    //     }
    //     else
    //     {
    //         BeatMiss();
    //     }
    //     beatReceived = false;
    //     typingReceived = false;
    // }
    // void OnDestroy()
    // {
    //     Conductor.instance.SendTimedBeat -= ConductorBeatHandler;
    //     TypingInput.instance.SendBeat -= TypingBeatHandler;
    // }
}
