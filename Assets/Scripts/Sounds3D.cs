using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds3D : MonoBehaviour
{
    
    public Sound[] sounds;
    private List<AudioSource> _audioSources;
    void Awake(){
        _audioSources = new List<AudioSource>();
        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(s.source);
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatial_blend;
            s.source.minDistance = 1;
            s.source.maxDistance = s.maxDistance;
        }
    }

    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
            return;
        s.source.volume = 0;
        s.source.Play();
    }

    public void Stop(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
            return;
        s.source.Stop();
    }

    public bool isPlaying(string name){
        foreach (var a_s in _audioSources){
            if (a_s.clip.name == name){
                if(a_s.isPlaying)
                    return true;
            }
        }

        return false;
    }
}
