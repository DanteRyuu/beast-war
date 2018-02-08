using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("GameData")]
public class GameData {

    public static GameData Instance = new GameData();
    private static string filePath = Application.persistentDataPath + "/Save.xml";

    private static bool isLoaded;
    public delegate void LoadEvent();
    public static event LoadEvent OnLoad;

    [XmlElement("LocationX")]
    public float locationX;
    [XmlElement("LocationZ")]
    public float locationZ;

    [XmlElement("PlayerHP")]
    public int playerHP;

    [XmlElement("PlayerCoins")]
    public int playerCoins;

    public static void Init()
    {
        PlayerResources.OnChange += UpdateHP;
        PlayerResources.OnCoinsChange += UpdateCoins;
    }

    public static void SaveData()
    {
        // Save game state into XML
        var serializer = new XmlSerializer(typeof(GameData));
        var stream = new FileStream(filePath, FileMode.Create);
        serializer.Serialize(stream, Instance);
        stream.Close();

        Debug.Log("Game saved here: " + filePath);
    }

    public static void LoadData()
    {
        if (File.Exists(filePath))
        {
            // Load info from XML
            var serializer = new XmlSerializer(typeof(GameData));
            var stream = new FileStream(filePath, FileMode.Open);
            try
            {
                Instance = serializer.Deserialize(stream) as GameData;
                stream.Close();

                isLoaded = true;
                if (OnLoad != null)
                    OnLoad();
            }
            catch (SystemException e)
            {
                stream.Close();
                NewData();
                SaveData();
            }
        }
        else
        {
            NewData();
            SaveData();
        }
    }

    public static void NewData()
    {
        Instance.locationX = 0;
        Instance.locationZ = 0;
        Instance.playerCoins = 0;
        Instance.playerHP = 100;

        if (OnLoad != null)
            OnLoad();
    }

    public static void OnDataInit(LoadEvent callback)
    {
        OnLoad += callback;

        if (isLoaded == true)
        {
            callback();
        }
        else
        {
            Debug.Log("Game Data Not loaded!");
        }
    }

    static void UpdateHP()
    {
        Instance.playerHP = PlayerResources.hitPoints;
    }

    static void UpdateCoins()
    {
        Instance.playerCoins = PlayerResources.coins;
    }
}
