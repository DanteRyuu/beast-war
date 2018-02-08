using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour {

    public static int hitPoints { get; private set; }
    public static int coins { get; private set; }

    public delegate void PlayerHit();
    public static event PlayerHit OnChange;
    public delegate void CoinsObtained();
    public static event CoinsObtained OnCoinsChange;

    public PlayerResources() {}

    public static void Init()
    {
        //hitPoints = 100;
        //coins = 0;
        GameData.OnLoad += UpdateLoadedData;
    }

    public static void ReduceHealth(int health)
    {
        hitPoints = hitPoints - ((health <= hitPoints) ? health : hitPoints);
        Debug.Log("HP reduced to " + hitPoints);
        if (OnChange != null)
        {
            OnChange();
        }
    }

    public static void CollectCoins(int addCoins)
    {
        coins += addCoins;
        Debug.Log("Coins increased to " + coins);
        if ( OnCoinsChange != null)
        {
            OnCoinsChange();
        }
    }

    static void UpdateLoadedData()
    {
        hitPoints = GameData.Instance.playerHP;
        coins = GameData.Instance.playerCoins;
    }
}
