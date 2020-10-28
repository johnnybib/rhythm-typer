using UnityEngine;
[System.Serializable] 
public class Settings
{
    public float audioLatency;
    public float visualLatency;
    public float relativePrecision;
    public float absolutePrecision;
    public bool useAbsolutePrecision;
    public Settings()
    {
        audioLatency = 0;
        visualLatency = 0;
        relativePrecision = 0.5f;
        absolutePrecision = 0.1f;
        useAbsolutePrecision = false;
    }
}