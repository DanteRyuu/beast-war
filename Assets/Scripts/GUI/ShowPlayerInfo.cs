using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerInfo : MonoBehaviour {

    public int maxHP = 100;

    public Text coinText;
    public Slider hpSlider;

	// Use this for initialization
	void Start () {
        UpdateHP();
        UpdateCoins();
        PlayerResources.OnChange += UpdateHP;
        PlayerResources.OnCoinsChange += UpdateCoins;
	}
	
	void UpdateHP() {
        Debug.Log("HP: " + PlayerResources.hitPoints);
        Debug.Log("max HP: " + maxHP);
        hpSlider.value = (float)PlayerResources.hitPoints / (float)maxHP;
        Debug.Log("crt HP: " + hpSlider.value);
    }

    void UpdateCoins()
    {
        coinText.text = PlayerResources.coins.ToString();
    }
}
