using UnityEngine;
using System.Collections;
namespace Frostie
{
    public class FrostieSoundManager : MonoBehaviour
    {
        public AudioClip Jump;
        public AudioClip Walk;
        private bool isWalking;

        public AudioClip Melting;
        public AudioClip FreezeGround;

        public AudioClip Death;
        public AudioClip DeathInFire;

        public void playJumpSound()
        {
            if (GetComponent<AudioSource>().isPlaying)
            {
                if (GetComponent<AudioSource>().clip.name.Equals(Jump.name))
                    return;
                else
                    GetComponent<AudioSource>().Stop();
            }
            GetComponent<AudioSource>().PlayOneShot(Jump);
        }

        public void playWalkingSound(float movement)
        {
            if (Mathf.Abs(movement) >= 0.1)
            {
                if (!isWalking)
                {
                    GetComponent<AudioSource>().clip = Walk;
                    GetComponent<AudioSource>().loop = true;
                    GetComponent<AudioSource>().Play();
                    isWalking = true;
                }
            }
            else
            {
                if (isWalking && GetComponent<AudioSource>().clip.name.Equals(Walk.name))
                {
                    GetComponent<AudioSource>().Stop();
                }
                GetComponent<AudioSource>().loop = false;
                isWalking = false;
            }
        }

        public void playMeltingSound()
        {
            if (GetComponent<AudioSource>().isPlaying)
            {
                if (GetComponent<AudioSource>().clip.name.Equals(Melting.name))
                    return;
                else
                    GetComponent<AudioSource>().Stop();
            }
            GetComponent<AudioSource>().PlayOneShot(Melting);
        }

        public void playFreezingGroundSound()
        {
            if (GetComponent<AudioSource>().isPlaying)
            {
                if (GetComponent<AudioSource>().clip.name.Equals(FreezeGround.name))
                    return;
                else
                    GetComponent<AudioSource>().Stop();
            }
            GetComponent<AudioSource>().PlayOneShot(FreezeGround);
        }

        public void playDeathSound()
        {
            isWalking = false;
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(Death);
        }

        public void playDeathInFireSound()
        {
            isWalking = false;
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(DeathInFire);
        }
    }
}