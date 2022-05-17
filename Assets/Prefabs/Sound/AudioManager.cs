using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using Random = System.Random;

public class AudioManager : MonoBehaviour{ 
    public Sound[] sounds;
    public static AudioManager instance;
    public Slider volumeSlider;
    
    private float currentMusic;

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
        
        if (OptionsInGame.instance.musicSettings != currentMusic){
            currentMusic = OptionsInGame.instance.musicSettings;
            foreach (var a_s in sounds){
                if(a_s.source.isPlaying) 
                    a_s.source.volume = currentMusic;
            }
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
                    Stop(x.name);
                }
            }
        }
        while(true)
        {
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
    
    public void AdjustVolume(){
        PlayerPrefs.SetFloat("volumeFromSlider", volumeSlider.value);
        foreach (var sound in  sounds){
            sound.source.volume = PlayerPrefs.GetFloat("volumeFromSlider");
        }

       
    }
}
