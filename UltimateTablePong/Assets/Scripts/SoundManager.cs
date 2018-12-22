using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private AudioClip sfxHitPin1Clip;

    private GameObject introMusic;
    private GameObject inGame1;
    private GameObject inGame2;
    private GameObject endGame;

    private GameObject currentSong = null;

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
        introMusic = GameObject.Find("IntroMusic");
        inGame1 = GameObject.Find("InGame1Music");
        inGame2 = GameObject.Find("InGame2Music");
        endGame = GameObject.Find("EndClipMusic");
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
        introMusic.GetComponent<AudioSource>().Stop();
        inGame1.GetComponent<AudioSource>().Stop();
        inGame2.GetComponent<AudioSource>().Stop();
        endGame.GetComponent<AudioSource>().Stop();
        switch (name)
        {
            case "mainMenu":
                introMusic.GetComponent<AudioSource>().Play();
                currentSong = introMusic;
                break;
            case "inGame":
                switch (chosenSong)
                {
                    case "Barge":
                        inGame1.GetComponent<AudioSource>().Play();
                        currentSong = inGame1;
                        break;
                    case "Moskito":
                        inGame2.GetComponent<AudioSource>().Play();
                        currentSong = inGame2;
                        break;
                }
                break;
            case "gameEnd":
                endGame.GetComponent<AudioSource>().Play();
                currentSong = endGame;
                break;
        }
    }

    public void changePitchCurrentSong()
    {
        if (currentSong.GetComponent<AudioSource>().pitch < lastDesiredPitch)
        {
            Debug.Log("Nice");
            currentSong.GetComponent<AudioSource>().pitch = currentSong.GetComponent<AudioSource>().pitch + 0.1F;
        }
        else if (currentSong.GetComponent<AudioSource>().pitch > lastDesiredPitch)
        {
            Debug.Log("Nicr");
            currentSong.GetComponent<AudioSource>().pitch  = currentSong.GetComponent<AudioSource>().pitch - 0.1F;
        }
    }

    // Update is called once per frame
    void Update()
    {
        changePitchCurrentSong();

        introMusic.GetComponent<AudioSource>().volume = musicVolume;
        inGame1.GetComponent<AudioSource>().volume = musicVolume;
        inGame2.GetComponent<AudioSource>().volume = musicVolume;
        endGame.GetComponent<AudioSource>().volume = musicVolume;
    }
}
