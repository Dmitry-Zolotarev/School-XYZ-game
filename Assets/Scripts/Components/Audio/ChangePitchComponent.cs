using UnityEngine;

public class ChangePitchComponent : MonoBehaviour
{
    private AudioSource musicPlayer;
    void Start()
    {
        var musicPlayerObject = GameObject.FindGameObjectWithTag("MusicPlayer");
        musicPlayer = musicPlayerObject?.GetComponent<AudioSource>();
    }
    public void ChangeMusicPitch(int k) 
    {
        if(musicPlayer != null) musicPlayer.pitch = k / 100f;
    } 
}
