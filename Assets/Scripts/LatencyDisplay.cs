using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LatencyDisplay : MonoBehaviour
{
    public TextMeshProUGUI latencyText;

    void Start()
    {
        latencyText.text = string.Format("Audio Latency: {0:0.000} s, Visual Latency: {1:0.000} s", SaveLoad.settings.audioLatency, SaveLoad.settings.visualLatency);
    }
}
