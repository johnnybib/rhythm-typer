using UnityEngine;
[CreateAssetMenu(menuName="Song")]
public class Song : ScriptableObject
{
    public AudioClip clip;
    public double bpm;
}