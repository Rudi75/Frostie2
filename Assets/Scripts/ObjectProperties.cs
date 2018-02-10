using System.Collections.Generic;
using UnityEngine;

namespace Frostie
{
    public class ObjectProperties : MonoBehaviour
    {

        public List<Enums.ObjectType> types;

        public bool contains(params Enums.ObjectType[] requiredTypes)
        {
            foreach (var requiredType in requiredTypes)
            {
                if (!types.Contains(requiredType))
                {
                    return false;
                }
            }
            return true;
        }
    }
}