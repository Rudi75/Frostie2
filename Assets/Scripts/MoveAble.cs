using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frostie
{
    [RequireComponent(typeof(Collider2D))]
    public class MoveAble : MonoBehaviour
    {

        [SerializeField] private bool isSlippery;
        [SerializeField] private LayerMask collisionLayers;

        private new Collider2D collider;

        private void Start()
        {
            collider = GetComponent<Collider2D>();
        }

        public bool isMoveable(Vector2 direction)
        {
            return (isSlippery || isOnSlipperyGround()) && MovementHelper.canMove(collider, direction, collisionLayers);
        }



        private bool isOnSlipperyGround()
        {
            Vector3 bottomCenterPosition = collider.bounds.center;
            bottomCenterPosition.y -= collider.bounds.extents.y;
            Vector3 bottomLeftPosition = bottomCenterPosition;
            bottomLeftPosition.x = collider.bounds.min.x + 0.1f;
            Vector3 bottomRightPosition = bottomCenterPosition;
            bottomRightPosition.x = collider.bounds.max.x - 0.1f;
            return hitsSlipperyObject(bottomCenterPosition, Vector2.down)
                && hitsSlipperyObject(bottomLeftPosition, Vector2.down)
                && hitsSlipperyObject(bottomRightPosition, Vector2.down);
        }

        private bool hitsSlipperyObject(Vector2 position, Vector2 direction)
        {
           return  CollisionHelper.hitsObject(position, direction, collisionLayers, Enums.ObjectType.SOLID, Enums.ObjectType.SLIPPERY);
        }
    }
}