using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public enum TestType {AUDIO, VISUAL, NONE}
public class LatencyCalculator : MonoBehaviour
{
    private Keyboard keyboard;
    public TestType testType;
    private double beatReceivedTime, keyReceivedTime;
    private bool beatReceived, keyReceived;
    private double runningSum;
    private float totalSamples;
    private double latency;
    private List<double> samples;
    private bool runTest;
    private float beatExpiryTime;
    
    void Start()
    {
        Conductor.instance.SendTimedBeat += ReceiveBeat;
        keyboard = Keyboard.current;
        keyboard.onTextInput += ReceiveKey;
        Conductor.instance.gameObject.GetComponent<GrooveObject>().grooveEnabled = false;
        runTest = false;
    }

    public void StartTest(TestType testType)
    {
        this.testType = testType;
        if(testType == TestType.AUDIO)
        {
            Conductor.instance.gameObject.GetComponent<GrooveObject>().grooveEnabled = false;
            Conductor.instance.Initialize();
            Conductor.instance.StartAudio();
            beatExpiryTime = (float)Conductor.instance.GetSecPerInput()*0.5f;
            runTest = true;
        }
        else   
        {
            Conductor.instance.gameObject.GetComponent<GrooveObject>().grooveEnabled = true;
            Conductor.instance.StopAudio();
            Conductor.instance.Initialize();
            Conductor.instance.PlayNoAudio();
            beatExpiryTime = (float)Conductor.instance.GetSecPerInput()*0.5f;
            runTest = true;
        }
        runningSum = 0;
        totalSamples = 0;
        samples = new List<double>();
    }
    public void ReceiveBeat(double songTime)
    {
        beatReceivedTime = songTime;
        beatReceived = true;
        if(keyReceived)
        {
            AddSample();
        }
        else
        {
            StartCoroutine(ExpireBeat());
        }
    }
    public void ReceiveKey(char key)
    {
        keyReceivedTime = Conductor.instance.songTime;
        keyReceived = true;
        if(beatReceived)
        {
            AddSample();
        }
        else
        {
            StartCoroutine(ExpireKey());
        }
    }

    private IEnumerator ExpireBeat()
    {
        yield return new WaitForSeconds(beatExpiryTime);
        beatReceived = false;
    }

    private IEnumerator ExpireKey()
    {
        yield return new WaitForSeconds(beatExpiryTime);
        keyReceived = false;
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
    private void AddSample()
    {
        //Conductor will automatically compensate for audio lag if it is set
        double diff = keyReceivedTime - beatReceivedTime;

        // if(testType == TestType.AUDIO)
        //     diff = keyReceivedTime - (beatReceivedTime);
        // else//Add audio latency to visual lag calculation to compensate for input lag   
        //     diff = keyReceivedTime - (beatReceivedTime + Conductor.instance.audioLatency);

        if(Math.Abs(diff) < 0.5)//Don't add huge diffs that will throw off the average
            samples.Add(diff);
        beatReceived = false;
        keyReceived = false;
    }
    public double GetCurrentLatency()
    {
        return latency;
    }

    public void StopTest()
    {
        runTest = false;
    }

    void OnDestroy()
    {
        Conductor.instance.SendTimedBeat -= ReceiveBeat;
        keyboard.onTextInput -= ReceiveKey;
    }
}