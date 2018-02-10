using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Frostie
{
    public abstract class TriggerAble : MonoBehaviour
    {
        [SerializeField] List<Trigger> triggers;
        [SerializeField] bool onlyActiveWhenTriggered;


        private bool activated_ = false;

        public void Start()
        {
            if (triggers.Count == 0)
            {
                Debug.LogError("No Trigger set on TriggerAble" + gameObject.name);
            }
            foreach (Trigger trigger in triggers)
            {
                trigger.addTarget(this);
            }
        }

        abstract protected void performAction();

        public void trigger()
        {
            bool allButtonspressed = true;
            foreach (Trigger trigger in triggers)// all buttons pressed?
            {
                if (!trigger.isActivated)
                    allButtonspressed = false;
            }
            if (allButtonspressed && !activated_)
            {
                performAction();
                activated_ = true;
            }
            else if (onlyActiveWhenTriggered && activated_)
            {
                performAction();
                activated_ = false;
            }
        }
    }

}