using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    //Singleton class
    public static AnimationManager Instance;
    public Animator camAnim;
    public Animator caracterAnim;
    public Animator AnimatedCaracterAnim;

    public GameObject characterModel;
    public GameObject animatedCharacterModel;
    public GameObject hat;
    public GameObject propellerHat;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    void Start()
    {
        
    }
    public void ActivateWinCharacterAnimation(bool win) 
    {
        characterModel.SetActive(false);
        animatedCharacterModel.SetActive(true);
        if (win)
        {
            AnimatedCaracterAnim.SetInteger("AnimState",1);
        }
        else 
        {           
            AnimatedCaracterAnim.SetInteger("AnimState", 2);
        }
    }
    public void HatPowerUp() 
    {
        caracterAnim.SetInteger("CharacterAnim",1);
        hat.SetActive(false);
        propellerHat.SetActive(true);
    }
    public void ResetCharacter() 
    {
        propellerHat.SetActive(false);
        hat.SetActive(true);
        animatedCharacterModel.SetActive(false);
        characterModel.SetActive(true);
    }

    // Update is called once per frame
    public void PlayCamAnimation()
    {
        camAnim.SetBool("CamAnim", true);
    }
    public void ResetCamAnimation()
    {
        camAnim.SetBool("CamAnim", false);
    }
}
