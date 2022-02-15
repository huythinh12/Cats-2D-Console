using UnityEngine;
using UnityEngine.Audio;



[System.Serializable]
public class Sound 
{
    public string name,type;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    // Start is called before the first frame update
    [HideInInspector]
    public AudioSource source;
    public bool loop;
    
}
