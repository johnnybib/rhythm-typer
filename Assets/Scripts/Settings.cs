using UnityEngine;
[System.Serializable] 
public class Settings
{
    public float audioLatency;
    public float visualLatency;
    public Settings()
    {
        audioLatency = 0;
        visualLatency = 0;
    }
}