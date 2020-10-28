using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ManualLatencySetupManager : MonoBehaviour
{

    public Song testSong;
    public Toggle useAbsolutePrecision;
    public Slider relativePrecision;
    public TextMeshProUGUI rpText;
    public Slider absolutePrecison;
    public TextMeshProUGUI apText;
    void Awake()
    {
        SaveLoad.Load();
    }
    // Start is called before the first frame update
    void Start()
    {
        SaveLoad.SettingsChanged += UpdateSettings;
        Conductor.instance.song = testSong;

        relativePrecision.SetValueWithoutNotify(SaveLoad.settings.relativePrecision);
        SetRPText(SaveLoad.settings.relativePrecision);
        absolutePrecison.SetValueWithoutNotify(SaveLoad.settings.absolutePrecision);
        SetAPText(SaveLoad.settings.absolutePrecision);
        useAbsolutePrecision.SetIsOnWithoutNotify(SaveLoad.settings.useAbsolutePrecision);
        UpdateSettings();
        Conductor.instance.Initialize();
        Conductor.instance.StartAudio();

    }

    // Update is called once per frame
    void UpdateSettings()
    {
        Conductor.instance.audioLatency = SaveLoad.settings.audioLatency;
        Conductor.instance.visualLatency = SaveLoad.settings.visualLatency;
        Conductor.instance.minPrecision = SaveLoad.settings.relativePrecision;
        Conductor.instance.minPrecisionAbsolute = SaveLoad.settings.absolutePrecision;
        Conductor.instance.useAbsolutePrecision = SaveLoad.settings.useAbsolutePrecision;
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

    public void SetRelativePrecision(float amount)
    {
        SaveLoad.settings.relativePrecision = amount;
        SetRPText(amount);
        SaveLoad.SaveSettings();
    }

    public void SetAbsolutePrecision(float amount)
    {
        SaveLoad.settings.absolutePrecision = amount;
        SetAPText(amount);
        SaveLoad.SaveSettings();
    }
    public void UseAbsolutePrecision(bool isChecked)
    {
        SaveLoad.settings.useAbsolutePrecision = isChecked;
        SaveLoad.SaveSettings();
    }

    public void SetRPText(float amount)
    {
        rpText.text = string.Format("Relative Precision: {0:0.000}", amount);
    }
    public void SetAPText(float amount)
    {
        apText.text = string.Format("Absolute Precision: {0:0.000}", amount);
    }
    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    void OnDestroy()
    {
        SaveLoad.SettingsChanged -= UpdateSettings;
    }

}
