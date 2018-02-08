using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager Instance;
    public static bool canAttack;

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

        SoundManager.PlayBackgroundMusic(GetComponentsInChildren<AudioSource>()[0]);
    }

    private void Init()
    {
        SoundManager.Init();
        ChangeAudioPanelStatus();
        canAttack = false;
        PlayerResources.Init();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeAudioPanelStatus();
        }
    }

    public bool GetAudioPanelStatus()
    {
        return GetComponentInChildren<Canvas>().enabled;
    }

    public void ChangeAudioPanelStatus()
    {
        Canvas audioCanvas = GetComponentInChildren<Canvas>();
        audioCanvas.enabled = !audioCanvas.enabled;
    }
}
