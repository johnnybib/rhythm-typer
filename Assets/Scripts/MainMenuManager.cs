using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Awake()
    {
        SaveLoad.Load();
        Debug.Log("Audio latency: " + SaveLoad.settings.audioLatency + ",Visual Latency: " + SaveLoad.settings.visualLatency);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void LatencyCalibration()
    {
        SceneManager.LoadScene("Latency Calibration");
    }

    public void ManualLatencySetup()
    {
        SceneManager.LoadScene("Manual Latency Setup");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
