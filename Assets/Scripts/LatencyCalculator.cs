using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class LatencyCalculator : MonoBehaviour
{
    private double beatReceivedTime;
    private double keyReceivedTime;
    public double runningSum = 0;
    public float totalSamples = 0;
    private float audioLatency;
    public bool testAudioLatency;
    private List<double> samples;
    void Awake()
    {
        samples = new List<double>();
    }
    void Start()
    {
        Conductor.instance.SendTimedBeat += ReceiveBeat;
        TypingInput.instance.SendBeat += ReceivePress;
        audioLatency = Conductor.instance.audioLatency;
    }
    public void ReceiveBeat(double songTime)
    {
        beatReceivedTime = songTime;
    }
    public void ReceivePress(double songTime)
    {
        keyReceivedTime = songTime;
        if(beatReceivedTime != 0)
        {
            AddSample();
        }
    }

    void Update()
    {
        totalSamples += samples.Count;
        foreach(double sample in samples)
        {
            runningSum += sample;
        }
        samples.Clear();
        Debug.Log("Latency: " + runningSum/totalSamples);
    }
    private void AddSample()
    {
        double diff;
        if(testAudioLatency)
            diff = keyReceivedTime - (beatReceivedTime);
        else    
            diff = keyReceivedTime - (beatReceivedTime + audioLatency);
        if(Math.Abs(diff) < 0.5)//Don't add huge diffs that will throw off the average
            samples.Add(diff);
        keyReceivedTime = 0;
        beatReceivedTime = 0;
    }
}