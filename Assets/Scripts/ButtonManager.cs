﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ButtonManager : MonoBehaviour {

    public Button newGameButton;
    public Button loadGameButton;
    public Button optionsButton;
    public Button exitGameButton;

    // Use this for initialization
    void Start () {
        newGameButton.onClick.AddListener(onNewGame);
        loadGameButton.onClick.AddListener(onLoadGame);
        loadGameButton.onClick.AddListener(onExitGame);
        optionsButton.onClick.AddListener(onOptionsButton);
    }

        void onNewGame()
    {
        SceneManager.LoadScene("MainScene");
        SoundManager.StopBackgroundMusic();
    }

    void onLoadGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    void onOptionsButton()
    {
        GameManager.Get().ChangeAudioPanelStatus();
    }

    void onExitGame()
    {
        Application.Quit();
    }
}
