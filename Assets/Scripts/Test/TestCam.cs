using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Test
{
    public class TestCam:MonoBehaviour
    {
        public Vector3 postionOffset = new Vector3(0, 2, -5);
        public Vector3 rotationOffset = Vector3.zero;

        public Transform target;
        
        [SerializeField] private float moveSmoothness = 5f;
        [SerializeField] private float rotationSmoothness = 5f;

        private void LateUpdate()
        {
            FollowTarget();
            RotateToTarget();
        }
        
        private void FollowTarget()
        {
            Vector3 position = target.TransformPoint(postionOffset);
            transform.position = Vector3.Lerp(transform.position, position, moveSmoothness * Time.deltaTime);
        }
        
        private void RotateToTarget()
        {
            Quaternion rotation = Quaternion.LookRotation((target.position - transform.position) + rotationOffset, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSmoothness * Time.deltaTime);
        }
    }
}