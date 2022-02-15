using UnityEngine.Audio;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    public static SoundManager instance;
    float currentMusicVolume;
    public static float saveCurrentMusicVolume;
    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        SetUpSounds();


    }
    public void SetUpSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            if (s.type == "Music")
            {
               currentMusicVolume = saveCurrentMusicVolume =s.source.volume =PlayerPrefs.GetFloat("Music");
            }
            else if (s.type == "SFX")
            {
                s.source.volume = PlayerPrefs.GetFloat("SFX");
            }
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound" + name + " not found");
            return;
        }
        s.source.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        Play("Maestro");

    }

    // Update is called once per frame
    void Update()
    {
        UpdateVolume();


    }

    private void UpdateVolume()
    {
            foreach (Sound s in sounds)
            {

                if (s.type == "Music")
                {
                currentMusicVolume= s.source.volume = PlayerPrefs.GetFloat("Music");
                }
                else if (s.type == "SFX")
                {
                    s.source.volume = PlayerPrefs.GetFloat("SFX");
                }

            }
        
       
    }

    public void FadeOutVolume()
    {
        if (GameManager.isLoadScene)
        {
            foreach (Sound s in sounds)
            {

                if (s.type == "Music")
                {
                    StartCoroutine(FadeOutMusic(s,currentMusicVolume));
                }
                else if (s.type == "SFX")
                {
                    s.source.volume = PlayerPrefs.GetFloat("SFX");
                }

            }
        }
        return;
     }
    public void FadeInVolume()
    {
        if (!GameManager.isLoadScene)
        {
            foreach (Sound s in sounds)
            {
              

                if (s.type == "Music")
                {
                    StartCoroutine(FadeInMusic());
                }
                else if (s.type == "SFX")
                {
                    s.source.volume = PlayerPrefs.GetFloat("SFX");
                }

            }
        }
        return;

    }
    IEnumerator FadeOutMusic(Sound s,float currentVolume)
    {
        float saveCurrentVolume = currentVolume;
        float smallVolume = saveCurrentVolume / 75f; 

        while (saveCurrentVolume >= 0)
        {
            saveCurrentVolume -= smallVolume;
            PlayerPrefs.SetFloat("Music", saveCurrentVolume);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        if (SceneManager.GetActiveScene().name == "LoadingScene")
           s.source.Stop();

    }
    IEnumerator FadeInMusic()
    {

        float increaVolume =0;
        float smallVolume = saveCurrentMusicVolume / 75f;
        while (increaVolume <= saveCurrentMusicVolume)
        {
            increaVolume += smallVolume;
            PlayerPrefs.SetFloat("Music", increaVolume);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }
}
