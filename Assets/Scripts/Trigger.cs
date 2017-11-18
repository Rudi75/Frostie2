using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public abstract class Trigger : MonoBehaviour
{
    [SerializeField] bool stayActive;

    private bool isActive_ = false;
    private List<TriggerAble> targets = new List<TriggerAble>();

    protected void onChange(bool activate)
    {
        if (activate)
        {
            isActivated = true;
        }
        else if (isActivated && !stayActive)
        {
            isActivated = false;
        }
    }


    public bool isActivated
    {
        get
        {
            return isActive_;
        }
        set
        {
            if(isActive_ != value)
            {
                isActive_ = value;
                foreach (TriggerAble target in targets)
                {
                    target.trigger();
                }
            }
        }
    }

    internal void addTarget(TriggerAble triggerAble)
    {
        targets.Add(triggerAble);
    }
}

