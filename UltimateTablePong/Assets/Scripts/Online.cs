using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Online : MonoBehaviour
{

    private SettingsMenu settingsMenu;
    private GameObject OnlineMenu;

    void Start()
    {
        GameObject settings = GameObject.FindGameObjectWithTag("Board");
        if (settings != null)
        {
            settingsMenu = settings.GetComponent<SettingsMenu>();
        }

        OnlineMenu = GameObject.Find("JoinOnlineMatch");
        OnlineMenu.SetActive(false);

    }

    public void DisplayOnlineMenu()
    {
        Time.timeScale = 0;
        OnlineMenu.SetActive(true);
    }

    public void HideOnlineMenu()
    {
        Time.timeScale = 1;
        OnlineMenu.SetActive(false);
    }

    public void DisplaySettingsMenu()
    {
        HideOnlineMenu();
        settingsMenu.DisplaySettingsMenu();
    }

    public void joinMatch(Text text)
    {
        HideOnlineMenu();
        settingsMenu.JoinOnlineGame(text.text);
    }
}