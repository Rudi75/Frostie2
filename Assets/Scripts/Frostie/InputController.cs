using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{


    void Update()
    {
        PlayerMovement movement = FrostiePartManager.instance.activePart.GetComponent<PlayerMovement>();
        CollisionController collisionController = FrostiePartManager.instance.activePart.GetComponent<CollisionController>();
        ThrowHeadScript throwHeadScript = FrostiePartManager.instance.activePart.GetComponent<ThrowHeadScript>();
        float inputX = Input.GetAxis("Horizontal");

        if (movement != null && collisionController != null)
        {

            if (Mathf.Abs(inputX) > 0 && collisionController.canMove(inputX))
            {
                movement.move(inputX);
            }
            else
            {
                movement.move(0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && collisionController.isGrounded)
            {
                movement.jump();
            }
        }

        if(throwHeadScript != null && Input.GetKeyDown(KeyCode.Q))
        {
            throwHeadScript.setForward();
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            FrostiePartManager.instance.recallHead();
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            FrostiePartManager.instance.decoupleMiddlePart();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            FrostiePartManager.instance.recallMiddlePart();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            FrostiePartManager.instance.switchPart();
        }

    }
}
