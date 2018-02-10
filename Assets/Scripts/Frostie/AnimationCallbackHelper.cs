using UnityEngine;
using System.Collections;

namespace Frostie
{
    public class AnimationCallbackHelper : MonoBehaviour
    {
        public void Kill()
        {
            Debug.Log("Kill");
            /*
            HealthScript livingBeeing = GetComponentInChildren<HealthScript>();
            if (livingBeeing != null)
            {
                livingBeeing.Die();
            }
            else
            {
                PartHealthScript part = GetComponentInParent<PartHealthScript>();
                part.Die();

            }
            */
        }

        public void Jump()
        {
            Debug.Log("Jump");

            PlayerMovement frostieMoveScript = FrostiePartManager.instance.activePart.GetComponent<PlayerMovement>();
            if (frostieMoveScript != null)
            {
                frostieMoveScript.doJump();
                FrostieSoundManager soundManager = transform.GetComponentInChildren<FrostieSoundManager>();
                if (soundManager == null) soundManager = transform.parent.GetComponentInChildren<FrostieSoundManager>();
                if (soundManager == null) soundManager = transform.parent.parent.GetComponentInChildren<FrostieSoundManager>();
                if (soundManager != null) soundManager.playJumpSound();
            }

        }
    }
}