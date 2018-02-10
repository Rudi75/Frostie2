using UnityEngine;
using System.Collections;

namespace Frostie
{
    public class ThrowHeadScript : MonoBehaviour
    {

        private int state;
        [SerializeField] private SpriteRenderer arrow;
        [SerializeField] private SpriteRenderer halfCircle;
        private int direction = 1;
        private float angle;
        private Vector3 scale;
        private bool forward = false;
        private bool backward = false;

        public float rotationPerFrame = 3;
        public float sizeChangePerFrame = 0.05f;

        public int throwForce = 19;
        public bool isThrowInProgress
        {
            get
            {
                return state > 0;
            }
        }

        void Start()
        {
            state = 0;

            angle = arrow.transform.localEulerAngles.z;

            arrow.enabled = false;
            halfCircle.enabled = false;
            scale = arrow.transform.localScale;

        }

        // Update is called once per frame
        void LateUpdate()
        {

            switch (state)
            {
                case 0:
                    {
                        angle = 0;
                        scale = new Vector3(1, 1, 1);
                        arrow.enabled = false;
                        halfCircle.enabled = false;

                        if (forward && FrostiePartManager.instance.getHeadClone() == null)
                        {
                            state = 1;
                        }
                        break;
                    }
                case 1:
                    {
                        arrow.enabled = true;
                        halfCircle.enabled = true;
                        arrow.transform.localScale = new Vector3(1, 1, 1);
                        angle += direction * rotationPerFrame;
                        arrow.transform.localEulerAngles = new Vector3(0, 0, angle);
                        if (angle > 70 || angle < -70)
                        {
                            direction *= -1;
                        }
                        if (forward)
                        {
                            PlayerMovement moveScript = FrostiePartManager.instance.activePart.GetComponent<PlayerMovement>();
                            if (moveScript.viewDirection < 0)
                            {
                                angle *= -1;
                            }
                            state = 2;
                        }
                        if (backward)
                        {
                            state = 0;
                        }
                        break;
                    }
                case 2:
                    {
                        if (backward)
                        {
                            state = 1;
                        }
                        if (forward)
                        {

                            GameObject headClone = FrostiePartManager.instance.decoupleHead();

                            float xValue = angle / -90;
                            float yValue = 1 - Mathf.Abs(angle / 90);

                            Vector2 throwVector = new Vector2(xValue / Mathf.Max(xValue, yValue), yValue / Mathf.Max(xValue, yValue));

                            headClone.GetComponent<Rigidbody2D>().AddForce(throwVector * throwForce * scale.x, ForceMode2D.Impulse);

                            state = 0;
                        }
                        scale += new Vector3(sizeChangePerFrame * direction, sizeChangePerFrame * direction, 0);
                        if (scale.x > 1 || scale.x < 0.1)
                        {
                            direction *= -1;
                        }
                        arrow.transform.localScale = scale;
                        break;
                    }
            }
            forward = false;
            backward = false;
        }
        public void setForward()
        {
            if (!FrostiePartManager.instance.isMelted)
            {
                forward = true;
            }
            else
            {
                Debug.Log("ThrowHead.forward not possible");
            }
        }
        public void setBackward()
        {
            if (state > 0)
            {
                backward = true;
            }
            else
            {
                Debug.Log("ThrowHead.backward not possible");
            }
        }
    }
}