﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum PlayerID
{
    Player1,
    Player2
}

public class Player : MonoBehaviour, OnActionBeatElement, OnInputBeatElement
{
    public PlayerID id;
    private string idString;
    public float chargeTime = 0.80f;
    public float chargeTimer = 0;
    protected bool isCharging = false;
    private string chargingMove;

    public Health health;
    public float maxLife = 1200;
    public float currentLife;
    public FightManager fightManager;               //Script managing fights, on the GameManager
    public int wins;
    public GameObject PlayerBody;

    public enum MoveType { HIT, REFLECT, LASER, GUARD, SPECIAL, NEUTRAL }     //List of moves

    public struct Move
    {
        public MoveType move;
        public Sprite sprite;
        public bool isCharged;
    }

    public Move[] buffer = new Move[InputTranslator.step];

    public Image[] inputsImage = new Image[InputTranslator.step];

    public int bufferLength;
    private int currentAction;

    public Animator animator;

    public GameObject round1Item;
    public GameObject round2Item;

    public Animator laserAnimator;


    protected virtual void Start()
    {
        customStart();
    }

    public void customStart()
    {
        wins = 0;
        currentLife = maxLife;
        Reset();

        foreach (Image image in inputsImage)
        {
            image.enabled = false;
        }

        InputTranslator.RegisterOnActionBeatElement(this);
        InputTranslator.RegisterOnInputBeatElement(this);
        idString = (id == PlayerID.Player1) ? "1" : "2";
    }

    public virtual void OnEnterInputBeat()
    {
        for(int i = 0; i<inputsImage.Length; i++)
        {
            inputsImage[i].transform.localScale = new Vector3(0.75f, 0.75f, 1);
        }

        Reset();
    }
    public void OnActionBeat()
    {
        inputsImage[currentAction++].enabled = false;
    }

    public virtual void Reset()
    {
        Debug.Log(InputTranslator.step);
        for (int i = 0; i < InputTranslator.step; i++) {     //initialising the buffer
            buffer[i].move = MoveType.NEUTRAL;
            buffer[i].isCharged = false;
            buffer[i].sprite = fightManager.neutralSprite;
        }
        foreach (Image image in inputsImage) {
            image.enabled = false;
        }

        animator.ResetTrigger("doDamage");
        animator.ResetTrigger("doDamageReflect");
        animator.ResetTrigger("doDamageLaser");

        bufferLength = 0;
        currentAction = 0;
    }

    public void BufferReset()
    {
        Debug.Log("RESET");
        buffer = new Move[InputTranslator.step];
        Reset();
    }

    void Update()
    {
        //Debug.Log(wins);

        if (InputTranslator.sequence == Sequence.INPUT)      
        {
            if (bufferLength < InputTranslator.step)
            {
                if (Input.GetButtonDown("HitKey" + idString) && bufferLength < InputTranslator.step)
                {
                    buffer[bufferLength].move = MoveType.HIT;
                    isCharging = true;
                    chargingMove = "HitKey" + idString;
                    buffer[bufferLength].sprite = fightManager.hitSprite;
                    Debug.Log("HIT " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.hitSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
                if (Input.GetButtonDown("ReflectKey" + idString) && bufferLength < InputTranslator.step)
                {
                    buffer[bufferLength].move = MoveType.REFLECT;
                    isCharging = true;
                    chargingMove = "ReflectKey" + idString;
                    buffer[bufferLength].sprite = fightManager.reflectSprite;
                    Debug.Log("REFLECT " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.reflectSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
                if (Input.GetButtonDown("LaserKey" + idString) && bufferLength < InputTranslator.step)
                {
                    buffer[bufferLength].move = MoveType.LASER;
                    isCharging = true;
                    chargingMove = "LaserKey" + idString;
                    buffer[bufferLength].sprite = fightManager.laserSprite;
                    Debug.Log("LASER " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.laserSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
                if (Input.GetButtonDown("GuardKey" + idString) && bufferLength < InputTranslator.step)
                {
                    buffer[bufferLength].move = MoveType.GUARD;
                    buffer[bufferLength].sprite = fightManager.guardSprite;
                    Debug.Log("GUARD " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.guardSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
                if (Input.GetButtonDown("SpecialKey" + idString) && bufferLength < InputTranslator.step)
                {
                    buffer[bufferLength].move = MoveType.SPECIAL;
                    buffer[bufferLength].sprite = fightManager.specialSprite;
                    Debug.Log("SPECIAL " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.specialSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
            }
            if (isCharging)
            {
                chargeTimer += Time.deltaTime;
                if(chargeTimer>chargeTime)
                {
                    if (bufferLength > 0)
                    {
                        if (!buffer[bufferLength - 1].isCharged)
                        {
                            inputsImage[bufferLength - 1].transform.localScale = new Vector3(0.9f, 0.9f, 1f);
                            buffer[bufferLength - 1].isCharged = true;
                            AkSoundEngine.PostEvent("SFX_Common_Charged", gameObject);
                        }
                    }
                }
                if(Input.GetButtonUp(chargingMove))
                {
                    isCharging = false;
                    chargeTimer = 0;
                }
                if(InputTranslator.sequence == Sequence.ACTION)
                {
                    isCharging = false;
                    chargeTimer = 0;
                }
            }
            else {
                if (Input.GetButtonDown("EraseKey" + idString)) {
                    if (bufferLength > 0) {
                        buffer[bufferLength - 1].move = MoveType.NEUTRAL;
                        buffer[bufferLength - 1].isCharged = false;
                        buffer[bufferLength - 1].sprite = fightManager.neutralSprite;
                        Debug.Log("NEUTRAL " + (bufferLength - 1));
                        inputsImage[bufferLength - 1].enabled = false;
                        inputsImage[bufferLength - 1].transform.localScale = new Vector3(0.75f, 0.75f, 1f);
                        bufferLength--;


                    }
                }
            }
        }
    }

    public void OnEnterActionBeat()
    {
        isCharging = false;
        chargeTimer = 0;
    }
    public virtual void OnInputBeat() {}


    // Return the last move made by this player
    public Move lastMove {
        get { return buffer[(bufferLength - 1 >= 0) ? bufferLength - 1 : 0]; }
    }

    public Move GetMove(int index)
    {
        index = Mathf.Clamp(index, 0, buffer.Length - 1);
        return buffer[index];
    }

    public void PlayAnim(Move move, bool cancel = false, bool takeDamage = false, bool player2DoLaser = false)
    {
        if (move.isCharged)
        {
            {
                AkSoundEngine.SetSwitch("Charged", "Yes",PlayerBody);
            }
        }

        if(!move.isCharged)
        { 
                AkSoundEngine.SetSwitch("Charged", "No", PlayerBody);
        }
            

        if (cancel) {
            switch (move.move) {
                case MoveType.HIT:
                    animator.SetTrigger("doFente");
                    AkSoundEngine.PostEvent("SFX_Common_Fente", gameObject);
                    break;
                case MoveType.REFLECT:
                    animator.SetTrigger("doReflect");
                    AkSoundEngine.PostEvent("SFX_Common_Reflect", gameObject);
                    break;
                case MoveType.LASER:
                    animator.SetTrigger("doLaser");
                    laserAnimator.Play("Anim_Laser_vs_laser", 0, 0);
                    AkSoundEngine.PostEvent("SFX_Common_Laser",gameObject);
                    break;
                case MoveType.SPECIAL:
                    animator.SetTrigger("doSpecial");
                    AkSoundEngine.PostEvent("SFX_Common_Special", gameObject);
                    break;
                case MoveType.GUARD:
                    animator.SetTrigger("doGuard");
                    AkSoundEngine.PostEvent("SFX_Common_Guard", gameObject);
                    break;
                default:
                    break;
            }
        }
        else if (takeDamage) {
            switch (move.move) {
                case MoveType.REFLECT:
                    animator.SetTrigger("doDamageReflect");
                    break;
                case MoveType.LASER:
                    // ici c'est quand le player fait le laser et se prend le reflect
                    AkSoundEngine.PostEvent("SFX_Robot_Laser_Damage", gameObject);
                    laserAnimator.Play("Anim_Laser_Reflected", 0, 0);
                    animator.SetTrigger("doDamageLaser");
                    break;
                default:
                    animator.SetTrigger("doDamage");
                    break;
            }
        }
        else {
            switch (move.move) {
                case MoveType.HIT:
                    animator.SetTrigger("doFente");
                    break;
                case MoveType.REFLECT:
                    if (player2DoLaser) {
                    }
                    animator.SetTrigger("doReflect");
                    break;
                case MoveType.LASER:
                    animator.SetTrigger("doLaser");
                    laserAnimator.Play("Anim_Laser", 0, 0);
                    break;
                case MoveType.SPECIAL:
                    animator.SetTrigger("doSpecial");
                    break;
                case MoveType.GUARD:
                    animator.SetTrigger("doGuard");
                    break;
                default:
                    break;
            }
        }
    }

    public void ResetWins()
    {
        wins = 0;
    }

    public void UpdateRoundCounter()
    {
        if (wins == 2) {
            round1Item.SetActive(true);
            round2Item.SetActive(true);
        }
        else if (wins == 1) {
            round1Item.SetActive(true);
            round2Item.SetActive(false);
        }
        else {
            round1Item.SetActive(false);
            round2Item.SetActive(false);
        }
        //ici boulons = wins
    }
}
