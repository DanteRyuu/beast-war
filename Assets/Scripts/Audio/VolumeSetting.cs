using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour {

    public Slider bgMusicSlider;
    public Slider ambientalSlider;
    public Slider sfxSlider;
    public Slider voiceSlider;

    // Use this for initialization
    public void Start()
    {
        ConfigureSlider(bgMusicSlider, SoundManager.AudioChannel.Music);
        ConfigureSlider(ambientalSlider, SoundManager.AudioChannel.Ambiental);
        ConfigureSlider(sfxSlider, SoundManager.AudioChannel.SFX);
        ConfigureSlider(voiceSlider, SoundManager.AudioChannel.Voice);

        SoundManager.SaveSettings();
    }

    private void ConfigureSlider(Slider slider, SoundManager.AudioChannel channel)
    {
        if (!slider)
            return;

        slider.value = SoundManager.GetVolume(channel);

        slider.onValueChanged.AddListener((float value) => {
            SoundManager.SetVolume(channel, value);
        });
    }

    // Update is called once per frame
    void Update () {
        SoundManager.SaveSettings();
    }
}
