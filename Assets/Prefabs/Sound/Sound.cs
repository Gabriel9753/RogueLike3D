using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound{
    public string name;
    public bool musicForRun;
    public AudioClip clip;
    
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    [Range(0f, 1f)] public float spatial_blend;
    [HideInInspector]public float _minDistance = 1;
    [Range(0f, 500f)] public float maxDistance;
    [HideInInspector]
    public AudioSource source;

    public bool loop;
}
