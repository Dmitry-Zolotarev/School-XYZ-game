using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private int frameRate;
    [SerializeField] private bool playLooping;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private UnityEvent onComplete;

    private SpriteRenderer renderer;

    private float secondsPerFrame, nextFrameTime;
    private int i;
    private bool isPlaying = true;
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        if (frameRate == 0) frameRate++;
        secondsPerFrame = 1f / frameRate;
        nextFrameTime = Time.time + secondsPerFrame;
        renderer.sprite = sprites[0];
    }
    private void Update()
    {
        if (isPlaying && Time.time > nextFrameTime) {
            i++;
            if (i == sprites.Length) 
            {
                if (playLooping) i = 0;
                else {
                    
                    isPlaying = false;
                    onComplete?.Invoke();
                    return;
                } 
            }  
            renderer.sprite = sprites[i];
            nextFrameTime += secondsPerFrame;
        }         
    }
}
