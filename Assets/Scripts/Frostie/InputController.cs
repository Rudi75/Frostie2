using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{


    void Update()
    {



        float inputX = Input.GetAxis("Horizontal");


        CollisionController collisionController = FrostiePartManager.instance.activePart.GetComponent<CollisionController>();
        PlayerMovement movement = FrostiePartManager.instance.activePart.GetComponent<PlayerMovement>();
        if (movement != null && collisionController != null)
        {
            if (collisionController.canMove(inputX))
            {
                movement.move(inputX);
            }
            else
            {
                movement.move(0);
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (movement != null && collisionController != null && collisionController.isGrounded)
            {
                movement.jump();
            }
        }



        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowHeadScript throwHeadScript = FrostiePartManager.instance.activePart.GetComponent<ThrowHeadScript>();
            if (throwHeadScript != null)
            {
                throwHeadScript.setForward();
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            FrostiePartManager.instance.recallHead();
        }

        if (Input.GetKeyDown(KeyCode.Y))
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

        if (Input.GetKeyDown(KeyCode.M))
        {
            FrostiePartManager.instance.changeMeltState();
        }

    }
}
