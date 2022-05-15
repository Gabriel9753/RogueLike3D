using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using Random = System.Random;

public class AudioManager : MonoBehaviour{ 
    public Sound[] sounds;
    public static AudioManager instance;

    private bool nextSoundReady = true;
    void Awake(){
        if (instance == null)
            instance = this;
        else{
            Destroy(gameObject);
            return;
        }
        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Update(){
        if(WorldManager.instance.startedGame){
            if(nextSoundReady)
                StartCoroutine(LoopAudio());
        }
    }

    IEnumerator LoopAudio(){
        nextSoundReady = false;
        Random random = new Random();
        sounds = sounds.OrderBy(x => random.Next()).ToArray();
        Sound s = Array.Find(sounds, sound => sound.musicForRun);
        float length = s.clip.length;
        foreach (var x in sounds){
            if (x.musicForRun){
                if (x.source.isPlaying){
                    print("stop" + x.name);
                    Stop(x.name);
                }
            }
        }
        while(true)
        {
            print("start" + s.name);
            s.source.Play();
            yield return new WaitForSeconds(length);
            nextSoundReady = true;
        }
    }
    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
            return;
        s.source.Play();
    }

    public void Stop(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
            return;
        s.source.Stop();
    }
}
