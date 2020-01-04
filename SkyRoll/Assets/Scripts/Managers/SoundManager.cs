using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Singleton class
    public static SoundManager Instance;

    public AudioSource backgroudMusic;
    public AudioSource otherSounds;

    public List<AudioClip> audioClips=new List<AudioClip>();

    public bool isSoundOn;
    bool isPlaying;
    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    void Start() 
    {
        isSoundOn = DataHandler.Instance.GetPlayerStatus("IsSoundOn");//check sound status
        isPlaying = false;
    }
    public void SoundOption() 
    {
        if (isSoundOn)
        {
            isSoundOn = false;
            DataHandler.Instance.SaveData("IsSoundOn",false);
            backgroudMusic.Stop();
        }
        else
        {
            isSoundOn = true;
            DataHandler.Instance.SaveData("IsSoundOn", true);
            backgroudMusic.Play();
        }
    }
    public void SoundStatus() 
    {
        if (isSoundOn)
        {
            if (!isPlaying) 
            {
                isPlaying = true;
                backgroudMusic.Play();            
            }
        }
        else
        {
            isPlaying = false;
            backgroudMusic.Stop();
        }
    }
    public void ItemCollected() 
    {
        if (isSoundOn) 
        {
            otherSounds.clip = audioClips[0];
            otherSounds.Play();
        }
        
    }
    public void PlayerBoosted()
    {
        if (isSoundOn)
        {
            otherSounds.clip = audioClips[1];
            otherSounds.Play();
        }           
    }
}
