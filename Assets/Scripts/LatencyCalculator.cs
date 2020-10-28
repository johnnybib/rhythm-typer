using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class LatencyCalculator : MonoBehaviour
{

    private double beatReceivedTime, keyReceivedTime;
    private bool beatReceived, keyReceived;
    private double runningSum;
    private float totalSamples;
    private double latency;
    private List<double> samples;
    private bool runTest;
    
    void Start()
    {
        Conductor.instance.BeatHit += BeatHandler;
        Conductor.instance.BeatMiss += BeatHandler;
        runTest = false;
    }

    public void StartTest()
    {
        runTest = true;
        runningSum = 0;
        totalSamples = 0;
        samples = new List<double>();
    }
    private void BeatHandler(double diff)
    {
        samples.Add(diff);
    }
    void Update()
    {
        if(runTest)
        {
            if(samples.Count > 0)
            {
                totalSamples += samples.Count;
                foreach(double sample in samples)
                {
                    runningSum += sample;
                }
                samples.Clear();
                latency = runningSum/totalSamples;
            }
        }

    }
    public double GetCurrentLatency()
    {
        return latency;
    }
    public int GetNumSamples()
    {
        return (int)totalSamples;
    }

    public void StopTest()
    {
        runTest = false;
    }

    void OnDestroy()
    {
        Conductor.instance.BeatHit -= BeatHandler;
        Conductor.instance.BeatMiss -= BeatHandler;
    }
}