﻿using UnityEngine;
using System.Collections;
using KindsOfDeath = Assets.Scripts.Utils.Enums.KindsOfDeath;
using Faces = Assets.Scripts.Utils.Enums.Faces;

public class FrostieAnimationManager : MonoBehaviour {

    private Animator animator;
 
    private FrostieSoundManager frostieSound;
    

    public void Awake()
    {
        animator = GetComponentInParent<Animator>();
        frostieSound = GetComponent<FrostieSoundManager>();

    }

    
    public void animateWalking(bool isWalking)
    {
        animator.SetBool("IsWalking", isWalking);
    }

    public void animateMelting(bool isMelted)
    {
        animator.SetBool("isMelted", isMelted);
        frostieSound.playMeltingSound();
    }

   public void animateDeath(KindsOfDeath deathKind)
 {
     switch (deathKind)
     {
         case KindsOfDeath.Normal:
             animator.SetTrigger("Death");
             frostieSound.playDeathSound();
             break;
         case KindsOfDeath.InFire:
             animator.SetTrigger("DeathInFire");
             frostieSound.playDeathInFireSound();
             break;
         case KindsOfDeath.Squeezed:
             break;
     }
 }

    public void animateFacing(Faces face)
    {
        switch (face)
        {
            case Faces.FRONT:
                animator.SetTrigger("TurnFront");
                break;
            case Faces.SIDE:
                animator.SetTrigger("TurnSide");
                break;
            case Faces.BACK:
                animator.SetTrigger("TurnBack");
                break;
        }
    }

    public void animateJump()
    {
        animator.SetTrigger("Jump");
        //frostieSound.playJumpSound();
    }

}
