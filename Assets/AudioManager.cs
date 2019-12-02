using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    void Start () {
        Play("Theme", true);
    }

    public void Play (string name, bool loop = false) {
        Sound s =  Array.Find(sounds, sound => sound.name == name);
        if(loop) s.source.loop = true;
        
        s.source.Play();
    }
}
