using UnityEngine;
using System.Collections;

namespace Frostie
{
    public class FrostieAnimationManager : MonoBehaviour
    {

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

        public void animateDeath(Enums.KindsOfDeath deathKind)
        {
            switch (deathKind)
            {
                case Enums.KindsOfDeath.Normal:
                    animator.SetTrigger("Death");
                    frostieSound.playDeathSound();
                    break;
                case Enums.KindsOfDeath.InFire:
                    animator.SetTrigger("DeathInFire");
                    frostieSound.playDeathInFireSound();
                    break;
                case Enums.KindsOfDeath.Squeezed:
                    break;
            }
        }

        public void animateFacing(Enums.Faces face)
        {
            switch (face)
            {
                case Enums.Faces.FRONT:
                    animator.SetTrigger("TurnFront");
                    break;
                case Enums.Faces.SIDE:
                    animator.SetTrigger("TurnSide");
                    break;
                case Enums.Faces.BACK:
                    animator.SetTrigger("TurnBack");
                    break;
            }
        }

        public void animateJump()
        {
            animator.SetTrigger("Jump");
        }

    }
}