using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Test
{
    public enum WheelPosition
    {
        Front,
        Rear,
        All
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject mesh;
        public WheelCollider collider;
        public WheelPosition position;
    }
    
    public class TestCar : MonoBehaviour
    {
        private Vector2 direction = Vector2.up;
        private Rigidbody rb;
        
        [Header("Settings")]
        [SerializeField] WheelPosition driveWheels = WheelPosition.Rear;
        [SerializeField,Tooltip("현재 활용 안됨")] float maxSpeed = 100f;
        [SerializeField] float maxAcceleration = 30f;
        [SerializeField] float breakAcceleration = 50f;
        [SerializeField] float maxSteeringAngle = 30f;
        [SerializeField] private float centerOfMassYoffset = -1f;
        [Header("Variables")]
        [SerializeField] private Vector2VariableSO playerInput;
        [SerializeField] private Vector3VariableSO playerVelocity;
        


        [Header("Wheels")] 
        [SerializeField] private Wheel[] wheels;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            rb.centerOfMass += Vector3.up * centerOfMassYoffset;
        }


        private void FixedUpdate()
        {
            ApplySteering();
            ApplyMovement();
            playerVelocity.Value = rb.linearVelocity;
            // ApplyBreak();
        }

        private void LateUpdate()
        {
            ApplyWheelAnimation();
        }

        private void ApplySteering()
        {
            foreach (var wheel in wheels)
            {
                if (wheel.position == WheelPosition.Front)
                {
                    wheel.collider.steerAngle = direction.x * maxSteeringAngle;
                }
            }
        }
        
        private void ApplyMovement()
        {
            foreach (var wheel in wheels)
            {   
                wheel.collider.motorTorque = direction.y * maxAcceleration;
                if (driveWheels == WheelPosition.All || wheel.position == driveWheels)
                {
                    wheel.collider.motorTorque = direction.y * maxAcceleration;
                }
            }
        }
        
        private void ApplyWheelAnimation()
        {
            foreach (var wheel in wheels)
            {
                Vector3 position;
                Quaternion rotation;
                wheel.collider.GetWorldPose(out position, out rotation);
                wheel.mesh.transform.position = position;
                wheel.mesh.transform.rotation = rotation;
            }
        }

        // private void ApplyBreak()
        // {
        //     foreach (var wheel in wheels)
        //     {
        //         if (wheel.position == WheelPosition.All || wheel.position == driveWheels)
        //         {
        //             wheel.collider.brakeTorque = direction.y * breakAcceleration;
        //         }
        //     }
        // }
        
public void OnMove(InputValue value)
{
    direction = value.Get<Vector2>();
    float margin = 0.25f;
    direction.x = direction.x > margin ? 1 : direction.x < -margin ? -1 : 0;
    direction.y = direction.y >= 0 ? 1 : -1;
    playerInput.Value = direction;
}
    }
}