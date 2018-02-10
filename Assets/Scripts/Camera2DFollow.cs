using UnityEngine;

namespace Frostie
{
    public class Camera2DFollow : MonoBehaviour
    {

        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float offsetZ;
        private Vector3 lastTargetPosition;
        private Vector3 currentVelocity;
        private Vector3 lookAheadPos;

        public BoxCollider2D Bounds;

        private Vector3 minPosition, maxPosition;

        public void Start()
        {
            minPosition = Bounds.bounds.min;
            maxPosition = Bounds.bounds.max;
            lastTargetPosition = target.position;
            offsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }

        // Update is called once per frame
        private void Update()
        {

            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - lastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else
            {
                lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
            var cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);

            aheadTargetPos.y = Mathf.Lerp(transform.position.y, target.position.y, 2 * Time.deltaTime);

            aheadTargetPos.x = Mathf.Clamp(aheadTargetPos.x, minPosition.x + cameraHalfWidth, maxPosition.x - cameraHalfWidth);
            // aheadTargetPos.y = Mathf.Clamp(aheadTargetPos.y, minPosition.y + GetComponent<Camera>().orthographicSize, maxPosition.y - GetComponent<Camera>().orthographicSize);
            // Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

            transform.position = aheadTargetPos; //newPos;

            lastTargetPosition = target.position;
        }

    }
}