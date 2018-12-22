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

    public float sfxVolume = 0.5F;
    public float musicVolume = 0.5F;
    public float ambiantVolume = 0.5F;
    private string chosenSong = "";

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
        sfxVolume = volume;
    }

    public void changeMusicVolume(float volume)
    {
        musicVolume = volume;
    }

    public void changeAmbiantVolume(float volume)
    {
        ambiantVolume = volume;
    }

    public void chooseInGameMusic(string songName)
    {
        chosenSong = songName;
    }

    public void playMusic(string name)
    {
        musicSource.pitch = 1f;
        switch (name)
        {
            case "mainMenu":
                musicSource.clip = Resources.Load<AudioClip>("Kick_Shock");
                break;
            case "inGame":
                switch (chosenSong)
                {
                    case "Barge":
                        musicSource.clip = Resources.Load<AudioClip>("Barge");
                        break;
                    case "Moskito":
                        musicSource.clip = Resources.Load<AudioClip>("Moskito");
                        break;
                }
                break;
            case "gameEnd":
                musicSource.clip = Resources.Load<AudioClip>("Detour_Sting");
                break;
        }
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        musicSource.volume = musicVolume;
    }
}
