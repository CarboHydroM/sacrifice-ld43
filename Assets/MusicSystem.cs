using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour {

    public AudioClip[] bgmClips;
    private AudioSource audioSrc;

	// Use this for initialization
	void Start () {
        audioSrc = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayClip(int index)
    {
        audioSrc.Stop();
        audioSrc.clip = bgmClips[index];
        audioSrc.Play();
    }

    public void Stop()
    {
        audioSrc.Stop();
    }
}
