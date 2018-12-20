using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private AudioClip sfxHitPin1Clip;

    private AudioSource sfxSource;
    private AudioSource musicSource;
    private AudioSource ambiantSource;

    private Dictionary<string, AudioSource> sfxSources;
    private Dictionary<string, AudioSource> musicSources;
    private Dictionary<string, AudioSource> ambiantSources;

    private float sfxVolume = 0.5F;
    private float musicVolume = 0.5F;
    private float ambiantVolume = 0.5F;

    System.Random rnd;

    // Use this for initialization
    void Start()
    {

        sfxSource = GetComponent<AudioSource>();
        musicSource = GetComponent<AudioSource>();

        rnd = new System.Random();
    }

    public void changeSfxVolume(float volume)
    {
        changeVolumeToDictionary(volume, sfxSources);
    }

    public void changeMusicVolume(float volume)
    {
        changeVolumeToDictionary(volume, musicSources);
    }

    public void changeAmbiantVolume(float volume)
    {
        changeVolumeToDictionary(volume, ambiantSources);
    }

    private void changeVolumeToDictionary(float volume, Dictionary<string, AudioSource> dictionary)
    {
        foreach (KeyValuePair<string, AudioSource> entry in dictionary)
        {
            entry.Value.volume = volume;
        }
    }

    private void AddSound(AudioSource audioSource, float volume, bool loop)
    {
        audioSource.volume = volume;
        audioSource.loop = loop;
    }

    private void AddSoundToDictionary(string name, AudioSource audioSource, Dictionary<string, AudioSource> dictionary)
    {
        dictionary.Add(name, audioSource);
    }

    public void playSfxNoise(string name)
    {
        /*switch (name)
        {
            case "bumperHit":
                int random = rnd.Next(1,3);
                switch (random)
                {
                    case 1:
                        sfxSource.pitch = 1.0f;
                        break;
                    case 2:
                        sfxSource.pitch = 0.90f;
                        break;
                }
                sfxSource.clip = Resources.Load<AudioClip>("bumperHit");
                break;
            case "wallHitSound":
                sfxSource.pitch = 1.0f;
                sfxSource.clip = Resources.Load<AudioClip>("wallHit");
                break;
            case "palletHitSound":
                sfxSource.pitch = 1.0f;
                sfxSource.clip = Resources.Load<AudioClip>("palletHit");
                break;
            case "destroyedSound":
                sfxSource.pitch = 0.80f;
                sfxSource.clip = Resources.Load<AudioClip>("wallHit");
                break;
        }
        sfxSource.Play();
        //sfxSource.pitch = 1.0f;*/
    }

    public void playSfxNoiseAtPosition(string name, Vector3 position)
    {
        playSoundAtPosition(name, position, sfxSources);
    }

    public void playMusic(string name)
    {
        musicSource.volume = 0.5f;
        musicSource.pitch = 1f;
        switch (name)
        {
            case "mainMenu":
                musicSource.clip = Resources.Load<AudioClip>("Kick_Shock");
                break;
            case "inGame":
                musicSource.clip = Resources.Load<AudioClip>("Barge");
                break;
            case "gameEnd":
                musicSource.clip = Resources.Load<AudioClip>("Detour_Sting");
                break;
        }
        musicSource.Play();
    }

    private void playSoundAtPosition(string name, Vector3 position, Dictionary<string, AudioSource> discionary)
    {
        AudioSource.PlayClipAtPoint(discionary[name].clip, position);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
