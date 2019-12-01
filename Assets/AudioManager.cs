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

            if (s.name == "Theme") s.source.loop = true;
        }
    }

    void Start () {
        Play("Theme");
    }

    public void Play (string name) {
        Sound s =  Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
