﻿using UnityEngine;
using System.Collections;


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
        /*
        FrostiePartManager partManager = transform.GetComponentInChildren<FrostiePartManager>();
        if(partManager == null)
            partManager = transform.parent.GetComponentInChildren<FrostiePartManager>();
        if(partManager == null)
        {
            partManager = transform.parent.parent.GetComponentInChildren<FrostiePartManager>();
        }
        GameObject frostie = partManager.getActivePart();
        FrostieMoveScript frostieMoveScript = frostie.GetComponent<FrostieMoveScript>();
        if (frostieMoveScript != null)
        {
            frostieMoveScript.Jump();
            FrostieSoundManager soundManager = transform.GetComponentInChildren<FrostieSoundManager>();
            if (soundManager == null) soundManager = transform.parent.GetComponentInChildren<FrostieSoundManager>();
            if (soundManager == null) soundManager = transform.parent.parent.GetComponentInChildren<FrostieSoundManager>();
            if (soundManager != null) soundManager.playJumpSound();
        }
        */
    }
}
