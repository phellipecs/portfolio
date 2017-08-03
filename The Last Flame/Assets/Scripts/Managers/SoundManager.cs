using UnityEngine;
using System.Collections;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource efxSource;
    public AudioSource musicSource;
    public AudioSource musicSource2;                     
    public float lowPitchRange = .95f;              
    public float highPitchRange = 1.05f;

    public AudioClip[] bgm;
    public AudioClip[] sfx;


    private void Start()
    {
        if(LevelManager.instance.currentScene == "Menu")
        {
            musicSource.clip = bgm[0];
            musicSource.volume = 0.5f;
            musicSource.Play();
        }
        musicSource.volume = 0.1f;
        musicSource2.volume = 0.35f;
        musicSource.loop = true;
        musicSource2.loop = true;
    }

    public void PlaySFX(AudioClip clip)
    {
        efxSource.clip = clip;

        efxSource.Play();
    }

    public void PlayBGM(AudioClip clip)
    {   
    
        musicSource.clip = clip;
        musicSource.Play();
        
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;

        efxSource.clip = clips[randomIndex];

        efxSource.Play();
    }
}
