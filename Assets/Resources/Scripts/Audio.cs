using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Audio : MonoBehaviour {
    public AudioClip main;
    public AudioClip lose;
    public AudioClip boss;
    public AudioSource audio;
    public float volume;
    // Use this for initialization
    void Start()
    {
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlaySound(AudioClip a)
    {
       
        audio.Stop();
        audio.volume = 1f;
        audio.clip = a;
        audio.Play();
    }
    public void Volume()
    {
             audio.volume =volume;
    }
        
}
