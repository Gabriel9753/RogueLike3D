using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioManagerMenu : MonoBehaviour
{    
    public Sound[] sounds;
    public static AudioManagerMenu instance;
    public Slider volumeSlider;
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

    void Start()
    {
        
        foreach (Sound s in sounds){
            if (PlayerPrefs.HasKey("volumeFromSlider")){
                s.source.volume = PlayerPrefs.GetFloat("volumeFromSlider");
                volumeSlider.value = PlayerPrefs.GetFloat("volumeFromSlider");
            }
            if (s.name == "Soundtrack"){
                s.source.Play();
            }
        }
    }
    

    public void AdjustVolume(){
        PlayerPrefs.SetFloat("volumeFromSlider", volumeSlider.value);
        foreach (var sound in  sounds){
            sound.source.volume = PlayerPrefs.GetFloat("volumeFromSlider");
        }

       
    }
}
