using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Frostie
{
    public class CollisionController : MonoBehaviour
    {
        [SerializeField] private LayerMask collisionLayers;

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
                return CollisionHelper.hitsObject(bottomCenterPosition, Vector2.down,  collisionLayers, Enums.ObjectType.SOLID)
                    || CollisionHelper.hitsObject(bottomLeftPosition, Vector2.down,  collisionLayers, Enums.ObjectType.SOLID)
                    || CollisionHelper.hitsObject(bottomRightPosition, Vector2.down,  collisionLayers, Enums.ObjectType.SOLID);
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
                    canMove = MovementHelper.canMove(aCollider, inputX > 0 ? Vector2.right : Vector2.left, collisionLayers);

                }
            }
            return canMove;
        }

        
    }
}


