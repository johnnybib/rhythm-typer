using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SongSelector : MonoBehaviour
{
    public TextMeshProUGUI songNameText;
    public AudioSource previewAudioSource;
    public List<Song> songs;
    private int currentSongIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentSongIndex = 0;
        UpdateUI();
    }

    public void SelectRight()
    {
        currentSongIndex++;
        if(currentSongIndex >= songs.Count)
        {
            currentSongIndex = 0;
        }
        UpdateUI();

    }
    public void SelectLeft()
    {
        currentSongIndex--;
        if(currentSongIndex < 0)
        {
            currentSongIndex = songs.Count - 1;
        }
        UpdateUI();
    }

    public Song GetSelectedSong()
    {
        return songs[currentSongIndex];
    }
    public void UpdateUI()
    {
        songNameText.text = GetSelectedSong().clip.name;
        previewAudioSource.Stop();
        previewAudioSource.clip = GetSelectedSong().clip;
        previewAudioSource.Play();
        
    }
}
