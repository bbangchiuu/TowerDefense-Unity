using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public enum TowerType
    {
        MachineGun =0,
        Rocker = 1
    }

    public static string menuScene = "MenuScene";
    public static string sceneGame = "SceneGame";

    public static string base_url = "https://bbangchiuu.000webhostapp.com/ServerTowerDefense/";
    public static string GetRanking = "Ranking.php";
    public static string AddPlayer = "AddPlayer.php";
}

public class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
