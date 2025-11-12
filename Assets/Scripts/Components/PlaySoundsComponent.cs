using System;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundsComponent : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private List<AudioData> sounds;
    public void Play(string id)
    {
        foreach(var audioData in sounds)
        {
            if (audioData.id == id) {
                source.PlayOneShot(audioData.clip);
                break;
            }
            
        }
    }
    [Serializable]
    public class AudioData
    {
        [SerializeField] private string _id;
        [SerializeField] private AudioClip _clip;
        public string id => _id;
        public AudioClip clip => _clip;
    }
}
