using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Frostie
{
    public static class CollisionHelper
    {
        private static float collisionDistance = 0.2f;

        public static Transform findHittingObject(Vector2 position, Vector2 direction, LayerMask collisionLayers, params Enums.ObjectType[] types)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(position, direction, collisionDistance, collisionLayers);
            for (int j = 0; j < hits.Length; j++)
            {
                ObjectProperties properties = hits[j].transform.GetComponent<ObjectProperties>();
                if (properties != null && properties.contains(types))
                {
                    return hits[j].transform;
                }
            }
            return null;
        }

        public static bool hitsObject(Vector2 position, Vector2 direction, LayerMask collisionLayers, params Enums.ObjectType[] types)
        {
            return findHittingObject(position,direction,collisionLayers,types) != null;
        }
    }
}
