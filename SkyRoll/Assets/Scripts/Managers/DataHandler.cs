using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{

    public static DataHandler Instance;
    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    public void SaveData(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    public void SaveData(string key, string value) 
    {
        PlayerPrefs.SetString(key,value);
    }
    public void SaveData(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }
    public void SaveData(string key, bool value)
    {
        int _value;
        if (value)
        {
            _value = 1;
        }
        else
        {
            _value = 0;//
        }
        SaveData(key, _value);
    }
    public float GetFloatData(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }
    public string GetStringData(string key)
    {
        return PlayerPrefs.GetString(key);
    }
    public int GetIntData(string key)
    {
        return PlayerPrefs.GetInt(key,0);
    }
    public bool GetBoolData(string key)
    {
        
        int _value = GetIntData(key);
        bool value;

        if (_value==1)
        {
            value = true;
        }
        else
        {
            value = false;
        }
        return value;
    }
    public int GetIntPlayerData(string key)
    {
        return PlayerPrefs.GetInt(key, 1);
    }
    public bool GetPlayerStatus(string key)
    {

        int _value = GetIntPlayerData(key);//0
        bool value;

        if (_value == 1)
        {
            value = true;
        }
        else
        {
            value = false;
        }
        return value;
    }
}
