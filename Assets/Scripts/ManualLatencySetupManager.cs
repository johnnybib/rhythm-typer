using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManualLatencySetupManager : MonoBehaviour
{

    public Song testSong;
    void Awake()
    {
        SaveLoad.Load();
    }
    // Start is called before the first frame update
    void Start()
    {
        SaveLoad.SettingsChanged += UpdateLatency;
        Conductor.instance.song = testSong;
        Conductor.instance.audioLatency = SaveLoad.settings.audioLatency;
        Conductor.instance.visualLatency = SaveLoad.settings.visualLatency;
        Conductor.instance.Initialize();
        Conductor.instance.StartAudio();
    }

    // Update is called once per frame
    void UpdateLatency()
    {
        Conductor.instance.audioLatency = SaveLoad.settings.audioLatency;
        Conductor.instance.visualLatency = SaveLoad.settings.visualLatency;
    }

    public void AdjustAudioLatency(float amount)
    {
        SaveLoad.settings.audioLatency += amount;
        SaveLoad.SaveSettings();
    }
    public void AdjustVisualLatency(float amount)
    {
        SaveLoad.settings.visualLatency += amount;
        SaveLoad.SaveSettings();
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    void OnDestroy()
    {
        SaveLoad.SettingsChanged -= UpdateLatency;
    }

}
