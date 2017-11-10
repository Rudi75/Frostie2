using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utils;

public class ObjectProperties : MonoBehaviour {

  public List<Enums.ObjectType> types;

    public bool contains(Enums.ObjectType type)
    {
        foreach (var item in types)
        {
            if(item.Equals(type))
            {
                return true;
            }
        }
        return false;
    }
}
