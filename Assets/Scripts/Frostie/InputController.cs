using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    private PlayerMovement movement;
    private CollisionController collisionController;
    

    void Start()
    {
        //TODO remove when implementing multiple frostie parts
        movement = GetComponentInChildren<PlayerMovement>();
        collisionController = GetComponentInChildren<CollisionController>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        if (Mathf.Abs(inputX) > 0 && collisionController.canMove(inputX))
        {
            movement.move(inputX);
        }else
        {
            movement.move(0);
        }

        if(Input.GetKeyDown(KeyCode.Space) && collisionController.isGrounded)
        {
            movement.jump();
        }
    }
}
