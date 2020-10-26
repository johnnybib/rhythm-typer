using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


//https://www.reddit.com/r/gamedev/comments/13y26t/how_do_rhythm_games_stay_in_sync_with_the_music/c78aawd/
public class Conductor : MonoBehaviour
{
    public event Action<double> SendVisualBeat = delegate { };
    public event Action<double> SendTimedBeat = delegate { };
    public static Conductor instance;
    public Song song;
    public AudioSource audioSource;
    public double songTime = 0;
    [Range(1, 4)]
    public int allowedInputPerBeat;
    private double previousFrameTime = 0;
    private double lastReportedPlayheadPosition = 0;
    private double secPerInput;//Allowed seconds per beat
    private double secPerBeat;//Allowed seconds per input (can allow multiple inputs per beat)
    private double lastInput;
    private double lastBeat;
    public float visualLatency;//0.15
    public float audioLatency;//0.09

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
    

    // Update is called once per frame
    void Update()
    {
        if(audioSource.isPlaying)
        {
            //Manually update song time
            songTime += AudioSettings.dspTime - previousFrameTime;
            previousFrameTime = AudioSettings.dspTime;
            //When we get a new report from the audiosource, update our songtime by averaging
            if(audioSource.time != lastReportedPlayheadPosition)
            {
                songTime = (songTime + audioSource.time)*0.5f;
                lastReportedPlayheadPosition = audioSource.time;
            }
            //Send visual beat before the song's beat happens so the video is in sync since the video lags slightly
            if(songTime + visualLatency > lastBeat + secPerBeat)
            {
                SendVisualBeat(songTime);           
                lastBeat += secPerBeat;
            }
            //Send timed beat (to check against user input) after the song's beat happens so the timed beat is in sync with the audio since the audio lags slightly
            if(songTime - audioLatency > lastInput + secPerInput)
            {
                SendTimedBeat(songTime);           
                lastInput += secPerInput;
            }
        }
    }
    public void Initialize()
    {
        audioSource.clip = song.clip;
        secPerBeat = 60f / (song.bpm);
        secPerInput = 60f / (song.bpm * (float)allowedInputPerBeat);
        previousFrameTime = AudioSettings.dspTime;
        lastReportedPlayheadPosition = 0;
        lastBeat = 0;
        lastInput = 0;
    }
    public void StartAudio()
    {
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    //For visual latency test
    public void PlayNoAudio()
    {
        StartAudio();
        audioSource.volume = 0;
    }

    public double GetSecPerInput()
    {
        return secPerInput;
    }
}

