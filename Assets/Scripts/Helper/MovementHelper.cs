using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Frostie
{
    public static class MovementHelper
    {

        private static bool hitsSolidUnmoveableObject(Vector2 position, Vector2 direction, LayerMask collisionLayers)
        {
            Transform hit = CollisionHelper.findHittingObject(position, direction, collisionLayers, Enums.ObjectType.SOLID);
            if (hit != null)
            {
                MoveAble moveAble = hit.GetComponent<MoveAble>();
                if (moveAble != null && moveAble.isMoveable(direction))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public static bool canMove(Collider2D aCollider, Vector2 direction, LayerMask collisionLayers)
        {
            float xPosition = direction == Vector2.right ? aCollider.bounds.max.x + 0.1f : aCollider.bounds.min.x - 0.1f;
            Vector2 topPosition = new Vector2(xPosition, aCollider.bounds.max.y - 0.1f);
            Vector2 middlePosition = new Vector2(xPosition, aCollider.bounds.center.y);
            Vector2 bottomPosition = new Vector2(xPosition, aCollider.bounds.min.y + 0.1f);
            if (hitsSolidUnmoveableObject(topPosition, direction, collisionLayers) || hitsSolidUnmoveableObject(middlePosition, direction, collisionLayers) || hitsSolidUnmoveableObject(bottomPosition, direction, collisionLayers))
            {
                return false;
            }
            return true;
        }
    }
}
