using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class LatencyTestManager : MonoBehaviour
{
    public Song audioTestSong;
    public Song visualTestSong;
    private TestType currentTest;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI latencyText;
    public TextMeshProUGUI controlsText;
    public LatencyCalculator latencyCalculator;
    void Awake()
    {
        SaveLoad.Load();
    }
    void Start()
    {
        messageText.text = "Latency Setup";
        latencyText.text = "";
        controlsText.text = "Start Audio Test";
        currentTest = TestType.NONE;
    }
    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ControlsClicked()
    {
        if(currentTest == TestType.NONE)
        {
            Conductor.instance.audioLatency = 0;
            Conductor.instance.visualLatency = 0;
            Conductor.instance.song = audioTestSong;
            StartAudioTest();
        }
        else if(currentTest == TestType.AUDIO)
        {
            
            SaveAudioLatency();
            Conductor.instance.audioLatency = SaveLoad.settings.audioLatency;
            Conductor.instance.visualLatency = 0;
            Conductor.instance.song = visualTestSong;
            StartVisualTest();
        }
        else
        {
            latencyCalculator.StopTest();
            SaveVisualLatency();
            Exit();
        }
    }
    public void SaveAudioLatency()
    {
        SaveLoad.settings.audioLatency = (float)latencyCalculator.GetCurrentLatency();
        SaveLoad.SaveSettings();
    }
    public void SaveVisualLatency()
    {
        SaveLoad.settings.visualLatency = (float)latencyCalculator.GetCurrentLatency();
        SaveLoad.SaveSettings();
    }

    public void StartAudioTest()
    {
        messageText.text = "Audio test: Press any key to the beat!";
        controlsText.text = "Save and Start Visual Test";
        currentTest = TestType.AUDIO;
        latencyCalculator.StartTest(currentTest);
    }

    public void StartVisualTest()
    {
        messageText.text = "Visual test: Press any key when the square flashes!";
        controlsText.text = "Done";
        currentTest = TestType.VISUAL;
        latencyCalculator.StartTest(currentTest);
    }

    void Update()
    {
        if(currentTest != TestType.NONE)
            latencyText.text = string.Format("Latency: {0:0.000} s", latencyCalculator.GetCurrentLatency());
    }
}