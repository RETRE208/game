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

    private GameObject previousSong = null;
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
        previousSong = null;
        currentSong = null;
        introMusic = GameObject.Find("IntroMusic");
        inGame1 = GameObject.Find("InGame1Music");
        inGame2 = GameObject.Find("InGame2Music");
        endGame = GameObject.Find("EndClipMusic");

        inGame1.GetComponent<AudioSource>().volume = 0;
        inGame2.GetComponent<AudioSource>().volume = 0;
        endGame.GetComponent<AudioSource>().volume = 0;
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
        if (introMusic != previousSong)
        {
            introMusic.GetComponent<AudioSource>().Stop();
        }
        if (inGame1 != previousSong)
        {
            inGame1.GetComponent<AudioSource>().Stop();
        }
        if (inGame2 != previousSong)
        {
            inGame2.GetComponent<AudioSource>().Stop();
        }
        if (endGame != previousSong)
        {
            endGame.GetComponent<AudioSource>().Stop();
        }
        previousSong = currentSong;
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
            currentSong.GetComponent<AudioSource>().pitch += 0.01F * Time.deltaTime;
        }
        else if (currentSong.GetComponent<AudioSource>().pitch > lastDesiredPitch)
        {
            currentSong.GetComponent<AudioSource>().pitch  -= 0.01F * Time.deltaTime;
        }
    }

    private void changePitchOnSongChange()
    {
        if (previousSong != null)
        {
            if (previousSong.GetComponent<AudioSource>().volume > 0)
            {
                previousSong.GetComponent<AudioSource>().volume -= 0.01F;
            }
            else if (previousSong.GetComponent<AudioSource>().volume == 0)
            {
                previousSong.GetComponent<AudioSource>().Stop();
            }
            if (currentSong.GetComponent<AudioSource>().volume < musicVolume)
            {
                currentSong.GetComponent<AudioSource>().volume += 0.01F;
            }
            else if (currentSong == introMusic)
            {
                introMusic.GetComponent<AudioSource>().volume = musicVolume;
                previousSong = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        changePitchCurrentSong();
        changePitchOnSongChange();
        if (previousSong == null)
        {
            currentSong.GetComponent<AudioSource>().volume = musicVolume;
        }
    }
}
