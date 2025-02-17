﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEventAnim : MonoBehaviour
{

    public GameObject PlayerBody1;
    public GameObject PlayerBody2;

    // WWise event for anim 

    //FOLEY Robot

    public void Foley_Robot_Guard()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Guard", gameObject);
    }

    public void Foley_Robot_Shield()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Shield", gameObject);
    }

    public void Foley_Robot_Fente()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Fente", gameObject);
    }

    public void Foley_Robot_Laser_Door_Open()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Laser_Door_Open", gameObject);
    }

    public void Foley_Robot_Laser_Door_Close()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Laser_Door_Close", gameObject);
    }

    public void Foley_Robot_SpecialAttack()
    {
        AkSoundEngine.PostEvent("Foley_Robot_SpecialAttack", gameObject);
    }

    public void Foley_Robot_Death()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Death", gameObject);
    }

    public void Foley_Robot_Iddle()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Iddle", gameObject);
    }

    public void Foley_Robot_Win()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Win", gameObject);
    }

    public void Foley_Robot_Whoosh()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Whoosh", gameObject);
    }

    public void Foley_Robot_Damage()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Damage", gameObject);
    }

    public void Foley_Robot_Lose_Arm()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Lose_Arm", gameObject);
    }

    public void Foley_Robot_Metal_Creaking()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Metal_Creaking", gameObject);
    }

    public void Foley_Robot_Fall()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Fall", gameObject);
    }

    public void Foley_Robot_Death_Metal_Creaking()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Death_Metal_Creaking", gameObject);
    }

    public void Foley_Robot_Iddle_Door()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Iddle_Door", gameObject);
    }

    public void Foley_Robot_Reflect_OpenDoor()
    {
        AkSoundEngine.PostEvent("Foley_Robot_Reflect_OpenDoor", gameObject);
    }


    // SFX Robot

    public void SFX_Robot_Laser_Shoot()
    {
        AkSoundEngine.PostEvent("SFX_Robot_Laser_Shoot", gameObject);
    }

    public void SFX_Robot_Fente()
    {
        AkSoundEngine.PostEvent("SFX_Robot_Fente", gameObject);
    }

    public void SFX_Robot_SpecialAttack()
    {
        AkSoundEngine.PostEvent("SFX_Robot_SpecialAttack", gameObject);
    }

    public void SFX_Robot_Death()
    {
        AkSoundEngine.PostEvent("SFX_Robot_Death", gameObject);
    }

    public void SFX_Robot_Particles()
    {
        AkSoundEngine.PostEvent("SFX_Robot_Particles", gameObject);
    }

    public void SFX_Robot_Projectile_Woosh()
    {
        AkSoundEngine.PostEvent("SFX_Robot_Projectile_Woosh", gameObject);
    }

    public void SFX_Robot_Laser_Impact_Reflect()
    {
        AkSoundEngine.PostEvent("SFX_Robot_Laser_Impact_Reflect", gameObject);
    }

    public void SFX_Robot_Laser_Reload()
    {
        AkSoundEngine.PostEvent("SFX_Robot_Laser_Reload", gameObject);
    }

    public void SFX_Robot_Explosion()
    {
        AkSoundEngine.PostEvent("SFX_Robot_Explosion", gameObject);
    }

    public void SFX_Robot_Shield()
    {
        AkSoundEngine.PostEvent("SFX_Robot_Shield", gameObject);
    }

    public void SFX_Robot_Laser_Damage()
    {
        AkSoundEngine.PostEvent("SFX_Robot_Laser_Damage", gameObject);
    }

    public void ElectricAmerican ()
    {
        AkSoundEngine.PostEvent("SFX_Common_Electric_Sparks_American", PlayerBody1);
    }

    public void ElectricJapan()
    {
        AkSoundEngine.PostEvent("SFX_Common_Electric_Sparks_Japan", PlayerBody2);
    }


}