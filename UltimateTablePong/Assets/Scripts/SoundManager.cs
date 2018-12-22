using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private AudioClip sfxHitPin1Clip;

    private AudioSource introMusic;
    private AudioSource inGame1;
    private AudioSource inGame2;
    private AudioSource endGame;

    private AudioSource currentSong;

    private AudioSource sfxSource;
    private AudioSource musicSource;
    private AudioSource ambiantSource;

    private Dictionary<string, AudioSource> sfxSources;
    private Dictionary<string, AudioSource> musicSources;
    private Dictionary<string, AudioSource> ambiantSources;

    public float sfxVolume = 0.5F;
    public float musicVolume = 0.5F;
    public float ambiantVolume = 0.5F;
    public float musicPitch = 1F;
    private string chosenSong = "";

    private float lastDesiredPitch = 1F;

    // Use this for initialization
    void Start()
    {
        introMusic = GameObject.Find("IntroMusic").GetComponent<AudioSource>();
        inGame1 = GameObject.Find("InGame1Music").GetComponent<AudioSource>();
        inGame2 = GameObject.Find("InGame2Music").GetComponent<AudioSource>();
        endGame = GameObject.Find("EndClipMusic").GetComponent<AudioSource>();
    }

    public void changeMusicPitch(float pitch)
    {
        lastDesiredPitch = pitch;
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
        introMusic.Stop();
        inGame1.Stop();
        inGame2.Stop();
        endGame.Stop();
        switch (name)
        {
            case "mainMenu":
                introMusic.Play();
                currentSong = introMusic;
                break;
            case "inGame":
                switch (chosenSong)
                {
                    case "Barge":
                        inGame1.Play();
                        currentSong = inGame1;
                        break;
                    case "Moskito":
                        inGame2.Play();
                        currentSong = inGame2;
                        break;
                }
                break;
            case "gameEnd":
                endGame.Play();
                currentSong = endGame;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (musicPitch < lastDesiredPitch)
        {
            musicPitch += 0.01F;
        }
        else if (musicPitch > lastDesiredPitch)
        {
            musicPitch -= 0.01F;
        }
        introMusic.pitch = musicPitch;
        inGame1.pitch = musicPitch;
        inGame2.pitch = musicPitch;
        endGame.pitch = musicPitch;

        introMusic.volume = musicVolume;
        inGame1.volume = musicVolume;
        inGame2.volume = musicVolume;
        endGame.volume = musicVolume;
    }
}
