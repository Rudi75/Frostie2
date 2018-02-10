using UnityEngine;
using System.Collections;

namespace Frostie
{
    public class FrostiePartManager : MonoBehaviour
    {

        public static FrostiePartManager instance { get; set; }

        private Transform frostieParent;
        private Transform head;
        private Transform middlePart;
        private Transform basePart;
        private PlayerMovement frostieBase;
        private GameObject headClone;
        private PlayerMovement middlePartClone;
        private PlayerMovement headAndMiddleClone;
        private FrostieAnimationManager frostieAnimationManager;

        public GameObject activePart { get; set; }
        private int activePartIndex;
        public GameObject HeadClonePrefab;
        public GameObject MiddleClonePrefab;
        public GameObject HeadAndMiddleClonePrefab;
        public Camera2DFollow cameraScript;

        private bool isMelted_;
        public bool isMelted
        {
            get
            {
                return isMelted_;
            }
        }


        void Start()
        {
            instance = this;
            frostieAnimationManager = GetComponent<FrostieAnimationManager>();
            foreach (Transform childTransform in transform)
            {
                foreach (Transform child in childTransform)
                {
                    if (child.name.Contains("Head"))
                        head = child;
                    else if (child.name.Contains("Middle"))
                        middlePart = child;
                    else if (child.name.Contains("Base"))
                        basePart = child;
                }

            }
            frostieParent = transform.parent;
            frostieBase = GetComponent<PlayerMovement>();
            activePart = frostieBase.gameObject;
            activePartIndex = 1;
            cameraScript.target = activePart.transform;
        }

        public GameObject decoupleHead()
        {
            if (headClone == null && !isMelted_)
            {

                if (headAndMiddleClone == null)
                {
                    headClone = Instantiate(HeadClonePrefab, head.position, Quaternion.identity) as GameObject;

                    head.gameObject.SetActive(false);

                    Vector3 scale = headClone.transform.localScale;
                    scale.x *= frostieBase.viewDirection;
                    headClone.transform.localScale = scale;

                    headClone.transform.parent = frostieParent;
                }
                else
                {
                    foreach (Transform child in headAndMiddleClone.transform)
                    {
                        foreach (Transform childTransform in child)
                        {


                            if (childTransform.name.Contains("Head"))
                            {
                                headClone = Instantiate(HeadClonePrefab, childTransform.position, Quaternion.identity) as GameObject;

                                Vector3 scale = headClone.transform.localScale;
                                scale.x *= headAndMiddleClone.viewDirection;
                                headClone.transform.localScale = scale;

                                headClone.transform.parent = frostieParent;
                            }
                            else if (childTransform.name.Contains("Middle"))
                            {
                                GameObject go = Instantiate(MiddleClonePrefab, childTransform.position, Quaternion.identity) as GameObject;
                                middlePartClone = go.GetComponent<PlayerMovement>();
                                Vector3 scale = middlePartClone.transform.localScale;
                                scale.x *= headAndMiddleClone.viewDirection;
                                middlePartClone.transform.localScale = scale;

                                middlePartClone.transform.parent = frostieParent;
                            }
                        }
                    }
                    Destroy(headAndMiddleClone.gameObject);
                }
                setActivePart(3);
                return headClone;
            }
            else
            {
                Debug.Log("decoupleHead not possible");
            }
            return null;
        }

        public GameObject decoupleMiddlePart()
        {
            if (middlePartClone == null && headAndMiddleClone == null && !isMelted_)
            {
                if (headClone != null)
                {
                    GameObject go = Instantiate(MiddleClonePrefab, middlePart.position, Quaternion.identity) as GameObject;
                    middlePartClone = go.GetComponent<PlayerMovement>();
                    middlePart.gameObject.SetActive(false);

                    Vector3 scale = middlePartClone.transform.localScale;
                    scale.x *= frostieBase.viewDirection;
                    middlePartClone.transform.localScale = scale;

                    middlePartClone.transform.parent = frostieParent;
                    setActivePart(2);
                    return middlePartClone.gameObject;
                }
                else
                {
                    GameObject go = (Instantiate(HeadAndMiddleClonePrefab, middlePart.position, Quaternion.identity) as GameObject);
                    headAndMiddleClone = go.GetComponent<PlayerMovement>();
                    middlePart.gameObject.SetActive(false);
                    head.gameObject.SetActive(false);

                    Vector3 scale = headAndMiddleClone.transform.localScale;
                    scale.x *= frostieBase.viewDirection;
                    headAndMiddleClone.transform.localScale = scale;

                    headAndMiddleClone.transform.parent = frostieParent;
                    setActivePart(2);
                    return headAndMiddleClone.gameObject;

                }

            }
            else
            {
                Debug.Log("decoupleMid not possible");
            }
            return null;
        }

        public void recallMiddlePart()
        {
            if (middlePartClone != null)
            {
                Destroy(middlePartClone.gameObject);
                middlePartClone = null;
            }
            if (headAndMiddleClone != null)
            {
                Destroy(headAndMiddleClone.gameObject);
                headAndMiddleClone = null;
                head.gameObject.SetActive(true);
            }

            middlePart.gameObject.SetActive(true);

            setActivePart(1);
        }



        public void recallHead()
        {
            if (middlePartClone != null)
            {
                GameObject go = Instantiate(HeadAndMiddleClonePrefab, middlePartClone.transform.position, Quaternion.identity) as GameObject;
                headAndMiddleClone = go.GetComponent<PlayerMovement>();
                middlePart.gameObject.SetActive(false);
                head.gameObject.SetActive(false);

                Vector3 scale = headAndMiddleClone.transform.localScale;
                scale.x *= middlePartClone.transform.localScale.x;
                headAndMiddleClone.transform.localScale = scale;

                headAndMiddleClone.transform.parent = frostieParent;

                Destroy(middlePartClone.gameObject);
                Destroy(headClone);
                setActivePart(2);
            }
            else if (headAndMiddleClone == null)
            {
                if (headClone != null)
                {
                    Destroy(headClone);
                    headClone = null;
                }
                head.gameObject.SetActive(true);
                setActivePart(1);
            }
        }

        public Transform getBasePart()
        {
            return basePart;
        }

        public Transform getHead()
        {
            return head;
        }

        public Transform getMiddlePart()
        {
            return middlePart;
        }

        public GameObject getHeadClone()
        {
            return headClone;
        }

        public GameObject getMiddlePartClone()
        {
            return middlePartClone.gameObject;
        }

        public GameObject getHeadAndMiddleClone()
        {
            return headAndMiddleClone.gameObject;
        }



        public void switchPart()
        {
            int nextIndex = activePartIndex;
            do
            {
                nextIndex++;
                if (nextIndex > 3)
                {
                    nextIndex = 1;
                }
            } while (setActivePart(nextIndex) == false);
        }

        public bool setActivePart(int part)
        {
            bool success = false;
            PlayerMovement moveScript = activePart.GetComponent<PlayerMovement>();
            if (moveScript != null)
            {
                moveScript.enabled = false;
            }
            if (part == 1)
            {

                activePart = frostieBase.gameObject;
                frostieBase.enabled = true;
                success = true;
            }
            else if (part == 2)
            {
                if (middlePartClone != null)
                {
                    activePart = middlePartClone.gameObject;
                    middlePartClone.enabled = true;
                    success = true;
                }

                if (headAndMiddleClone != null)
                {
                    activePart = headAndMiddleClone.gameObject;
                    headAndMiddleClone.enabled = true;
                    success = true;
                }
            }
            else if (part == 3)
            {
                if (headClone != null)
                {
                    activePart = headClone;
                    success = true;
                }
            }

            if (success)
            {
                activePartIndex = part;
            }
            return success;
        }


        public void changeMeltState()
        {

            if (headClone == null && middlePartClone == null && headAndMiddleClone == null && !activePart.GetComponent<ThrowHeadScript>().isThrowInProgress)
            {

                isMelted_ = !isMelted_;
                frostieAnimationManager.animateMelting(isMelted_);
            }
            else
            {
                Debug.Log("Melting not possibe");
            }
        }

        public void FixedUpdate()
        {
            if (cameraScript != null)
            {
                cameraScript.target = activePart.transform;
            }
            else
            {
                Debug.LogError("camera not set in FrostiePartManager!");
            }
        }
    }
}