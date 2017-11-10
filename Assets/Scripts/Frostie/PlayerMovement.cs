using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpHight = 40;

    private float movement;
    private bool isWalking = false;
    private bool grounded = false;

    private Rigidbody2D playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    public void move(float inputX)
    {
        float viewDirection = transform.localScale.x / Mathf.Abs(transform.localScale.x);

        if (viewDirection * inputX < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            viewDirection = inputX;
        }

        movement = speed * inputX;

        bool value = (movement != 0.0f);

        if (isWalking != value)
        {
            isWalking = value;

            FrostieAnimationManager frostieAnimationManager = GetComponent<FrostieAnimationManager>();
            if (frostieAnimationManager != null)
            {
                frostieAnimationManager.animateWalking(isWalking);
            }
            else
            {
                Animator animator = GetComponentInChildren<Animator>();
                animator.SetBool("IsWalking", isWalking);
            }
        }

        FrostieSoundManager frostieSoundManager = GetComponent<FrostieSoundManager>();
        if (frostieSoundManager != null)
        {
            frostieSoundManager.playWalkingSound(movement);
        }
    }

    public void jump()
    {
        playerRigidbody.AddForce(new Vector2(0, jumpHight), ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        float speedX = movement;
        float speedY = playerRigidbody.velocity.y;
        playerRigidbody.velocity = new Vector2(speedX, speedY);
    }
}
