using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


//https://www.reddit.com/r/gamedev/comments/13y26t/how_do_rhythm_games_stay_in_sync_with_the_music/c78aawd/
public class Conductor : MonoBehaviour
{
    public event Action<double> SendVisualBeat = delegate { };
    public event Action<string, double, Color> SendDebugBeat = delegate { };
    public event Action<double> BeatHit = delegate { };
    public event Action<double> BeatMiss = delegate { };
    public static Conductor instance;
    public Song song;
    public AudioSource audioSource;
    public double songTime = 0;
    [Range(1, 4)]
    public int checksPerBeat;

    [Tooltip("0: must be exactly on beat. 1: can be anywhere from exactly on beat to exactly half a beat off (worst precision possible")]
    [Range(0, 1)]
    public double minPrecision;
    public bool useAbsolutePrecision;
    public double minPrecisionAbsolute;
    private double previousFrameTime = 0;
    private double storedPreviousFrameTime = 0;
    private double lastReportedPlayheadPosition = 0;
    private double secPerCheck;//Allowed checks per second
    private double secPerBeat;
    private double lastCheck;
    private double lastBeat;
    private double lastCheckTime;
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
    
    void Start()
    {
        if(TypingInput.instance)
            TypingInput.instance.SendBeat += CheckHit;
    }
    // Update is called once per frame
    void Update()
    {
        if(audioSource.isPlaying)
        {
            //Manually update song time
            songTime += AudioSettings.dspTime - previousFrameTime;
            storedPreviousFrameTime = previousFrameTime;//To use to find delta time of last frame in case previousFrameTime is overwritten
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
                lastBeat += secPerBeat;//Need to increment by sec per beat
            }
            //Send timed beat (to check against user input) after the song's beat happens so the timed beat is in sync with the audio since the audio lags slightly
            if(songTime - audioLatency > lastCheck + secPerCheck)
            {
                lastCheck += secPerCheck;//Need to increment by sec per check
                lastCheckTime = songTime;
                SendDebugBeat("checkBeat", songTime, Color.yellow);
                SendDebugBeat("nextCheckTime", lastCheckTime + secPerCheck, Color.magenta);
            }
        }
    }

    public void CheckHit(double typingReceivedTime)
    {   
        double diffToPrevBeat = Math.Abs(typingReceivedTime - lastCheckTime);
        double diffToNextBeat = Math.Abs(typingReceivedTime - (lastCheckTime + secPerCheck));
        double minPrecisionToUse;
        if(useAbsolutePrecision)
            minPrecisionToUse = minPrecisionAbsolute;
        else
            minPrecisionToUse = secPerCheck * 0.5f * minPrecision;
        if(diffToPrevBeat < minPrecisionToUse)
        {
            BeatHit(diffToPrevBeat);
        }
        else if(diffToNextBeat < minPrecisionToUse)
        {
            BeatHit(diffToNextBeat);
        }
        else
        {
             Debug.Log("Miss: diff=" + 
                Math.Abs(typingReceivedTime - lastCheckTime) + " and "  +
                Math.Abs(typingReceivedTime - (lastCheckTime + secPerCheck)) + ". Should be less than " + minPrecisionToUse);
            if(diffToPrevBeat < diffToNextBeat)
                BeatMiss(diffToPrevBeat);
            else
                BeatMiss(diffToNextBeat);
        }
    }
    public void Initialize()
    {
        audioSource.clip = song.clip;
        secPerBeat = 60f / (song.bpm);
        secPerCheck = 60f / (song.bpm * (float)checksPerBeat);
        previousFrameTime = AudioSettings.dspTime;
        storedPreviousFrameTime = previousFrameTime;
        lastReportedPlayheadPosition = 0;
        lastBeat = 0;
        lastCheck = 0;
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
    public double GetSecPerBeat()
    {
        return secPerBeat;
    }
    public double GetSecPerCheck()
    {
        return secPerCheck;
    }

    public double GetLastFrameDSPTime()
    {
        return AudioSettings.dspTime - storedPreviousFrameTime;
    }

    void OnDestroy()
    {
        TypingInput.instance.SendBeat -= CheckHit;
    }
}

