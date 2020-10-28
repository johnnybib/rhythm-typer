using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public SongSelector songSelector;
    public GameObject gameplayUI;
    public GameObject gameplayMenuUI;
    void Awake()
    {
        SaveLoad.Load();
        Debug.Log("Audio latency: " + SaveLoad.settings.audioLatency + ",Visual Latency: " + SaveLoad.settings.visualLatency);
    }

    void Start()
    {
        Conductor.instance.audioLatency = SaveLoad.settings.audioLatency;
        Conductor.instance.visualLatency = SaveLoad.settings.visualLatency;
        Conductor.instance.minPrecision = SaveLoad.settings.relativePrecision;
        Conductor.instance.minPrecisionAbsolute = SaveLoad.settings.absolutePrecision;
        Conductor.instance.useAbsolutePrecision = SaveLoad.settings.useAbsolutePrecision;
        gameplayUI.SetActive(false);
    }

    public void StartGame()
    {
        Conductor.instance.song = songSelector.GetSelectedSong();
        songSelector.gameObject.SetActive(false);
        gameplayMenuUI.gameObject.SetActive(false);
        gameplayUI.SetActive(true);
        Conductor.instance.Initialize();
        Conductor.instance.StartAudio();
    }
    
    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}