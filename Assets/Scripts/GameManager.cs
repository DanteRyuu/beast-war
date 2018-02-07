using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager Instance;

    public static GameManager Get()
    {
        return Instance;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            Init();
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("GameManager Instance: " + (Instance == this));
        }

        SoundManager.PlayBackgroundMusic(GetComponentInChildren<AudioSource>());
    }

    private void Init()
    {
        SoundManager.Init();
        ChangeAudioPanelStatus();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeAudioPanelStatus();
        }
    }

    public void ChangeAudioPanelStatus()
    {
        Canvas audioCanvas = GetComponentInChildren<Canvas>();
        audioCanvas.enabled = !audioCanvas.enabled;
    }
}
