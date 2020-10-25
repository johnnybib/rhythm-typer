using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


//https://www.reddit.com/r/gamedev/comments/13y26t/how_do_rhythm_games_stay_in_sync_with_the_music/c78aawd/
public class Conductor : MonoBehaviour
{
    public event Action SendVisualBeat = delegate { };
    public event Action<double> SendTimedBeat = delegate { };
    public static Conductor instance;
    public Song song;
    public AudioSource audioSource;
    public double songTime = 0;
    [Range(1, 4)]
    public int allowedInputPerBeat;
    private double previousFrameTime = 0;
    private double lastReportedPlayheadPosition = 0;
    private double secPerInput;
    private double secPerBeat;
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
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = song.clip;
        secPerBeat = 60f / (song.bpm);
        secPerInput = 60f / (song.bpm * (float)allowedInputPerBeat);
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            StartSong();
        }
        //Manually update song time
        songTime += AudioSettings.dspTime - previousFrameTime;
        previousFrameTime = AudioSettings.dspTime;
        //When we get a new report from the audiosource, update our songtime by averaging
        if(audioSource.time != lastReportedPlayheadPosition)
        {
            songTime = (songTime + audioSource.time)*0.5f;
            lastReportedPlayheadPosition = audioSource.time;
        }
        if(songTime + visualLatency > lastBeat + secPerBeat)
        {
            SendVisualBeat();           
            lastBeat += secPerBeat;
        }
        if(songTime > lastInput + secPerInput)
        {
            SendTimedBeat(songTime);           
            lastInput += secPerInput;
        }
        
    }
    public void StartSong()
    {
        previousFrameTime = AudioSettings.dspTime;
        lastReportedPlayheadPosition = 0;
        lastBeat = 0;
        lastInput = 0;
        audioSource.Play();
    }
}
