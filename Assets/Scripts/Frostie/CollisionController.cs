using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionController : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private float collisionDistance = .2f;

    private Collider2D[] colliders;
    private Collider2D bottomCollider;


    private void Start()
    {
        colliders = GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (bottomCollider == null || colliders[i].transform.position.y < bottomCollider.transform.position.y)
            {
                bottomCollider = colliders[i];
            }
        }
    }


    public bool isGrounded
    {
        get
        {
            Vector3 bottomCenterPosition = bottomCollider.bounds.center;
            bottomCenterPosition.y -= bottomCollider.bounds.extents.y;
            Vector3 bottomLeftPosition = bottomCenterPosition;
            bottomLeftPosition.x = bottomCollider.bounds.min.x + 0.1f;
            Vector3 bottomRightPosition = bottomCenterPosition;
            bottomRightPosition.x = bottomCollider.bounds.max.x - 0.1f;
            return hitsSolidObject(bottomCenterPosition, Vector2.down)
                || hitsSolidObject(bottomLeftPosition, Vector2.down)
                || hitsSolidObject(bottomRightPosition, Vector2.down);
        }
    }

    public bool canMove(float inputX)
    {
        bool canMove = true;

        Vector2 direction = inputX > 0 ? Vector2.right : Vector2.left;
        


        for (int i = 0; i < colliders.Length && canMove; i++)
        {
            Collider2D aCollider = colliders[i];
            if (aCollider.isActiveAndEnabled)
            {
                float xPosition = inputX > 0 ? aCollider.bounds.max.x : aCollider.bounds.min.x;
                Vector2 topPosition = new Vector2(xPosition, aCollider.bounds.max.y - 0.1f);
                Vector2 middlePosition = new Vector2(xPosition, aCollider.bounds.center.y);
                Vector2 bottomPosition = new Vector2(xPosition, aCollider.bounds.min.y + 0.1f);
                if (hitsSolidObject(topPosition, direction) || hitsSolidObject(middlePosition, direction) || hitsSolidObject(bottomPosition, direction))
                {
                    canMove = false;
                }
            }
        }
        return canMove;
    }

    private bool hitsSolidObject(Vector2 topPosition, Vector2 direction)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(topPosition, direction, collisionDistance, collisionLayers);
        for (int j = 0; j < hits.Length; j++)
        {
            ObjectProperties properties = hits[j].transform.GetComponent<ObjectProperties>();
            if (properties != null && properties.contains(Enums.ObjectType.SOLID))
            {
                return true;
            }
        }
        return false;
    }
}


