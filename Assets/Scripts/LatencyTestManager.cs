using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
public enum TestType {AUDIO, VISUAL, NONE}

public class LatencyTestManager : MonoBehaviour
{
    public Song audioTestSong;
    public Song visualTestSong;
    public GameObject visualLatencyMetronome;
    private TestType currentTest;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI latencyText;
    public TextMeshProUGUI controlsText;
    public GameObject skipButton;
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
        visualLatencyMetronome.gameObject.SetActive(false);
        skipButton.SetActive(false);
    }
    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ControlsClicked()
    {
        if(currentTest == TestType.NONE)
        {
            StartAudioTest();
        }
        else if(currentTest == TestType.AUDIO)
        {
            
            SaveAudioLatency();
            StartVisualTest();
        }
        else
        {
            latencyCalculator.StopTest();
            SaveVisualLatency();
            Exit();
        }
    }

    public void SkipClicked()
    {
        if(currentTest == TestType.NONE)
        {
            StartAudioTest();
        }
        else if(currentTest == TestType.AUDIO)
        {
            StartVisualTest();
        }
        else
        {
            latencyCalculator.StopTest();
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
        skipButton.SetActive(true);

        Conductor.instance.audioLatency = 0;
        Conductor.instance.visualLatency = 0;
        Conductor.instance.song = audioTestSong;

        messageText.text = "Audio test: Press any key to the beat!";
        controlsText.text = "Save and Start Visual Test";

        currentTest = TestType.AUDIO;

        Conductor.instance.Initialize();
        Conductor.instance.StartAudio();
        latencyCalculator.StartTest();
    }

    public void StartVisualTest()
    {
        skipButton.SetActive(true);
        visualLatencyMetronome.gameObject.SetActive(true);

        Conductor.instance.audioLatency = 0;
        Conductor.instance.visualLatency = 0;
        Conductor.instance.song = visualTestSong;
        
        messageText.text = "Visual test: Press any key when the metronome hits and endpoint!";
        controlsText.text = "Done";

        currentTest = TestType.VISUAL;

        Conductor.instance.StopAudio();
        Conductor.instance.Initialize();
        Conductor.instance.PlayNoAudio();

        latencyCalculator.StartTest();
    }

    void Update()
    {
        if(currentTest != TestType.NONE)
            latencyText.text = string.Format("Latency: {0:0.000} s. Samples: {1}", latencyCalculator.GetCurrentLatency(), latencyCalculator.GetNumSamples());
    }
}