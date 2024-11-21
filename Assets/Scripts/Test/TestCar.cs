using System;
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
        private Vector2 direction = default;
        private Rigidbody rb;
        
        [Header("Settings")]
        [SerializeField] WheelPosition driveWheels = WheelPosition.Rear;
        [SerializeField] float maxSpeed = 100f;
        [SerializeField] float maxAcceleration = 30f;
        [SerializeField] float breakAcceleration = 50f;
        [SerializeField] float maxSteeringAngle = 30f;


        [Header("Wheels")] 
        [SerializeField] private Wheel[] wheels;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }


        private void FixedUpdate()
        {
            ApplySteering();
            ApplyMovement();
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
                if (wheel.position == WheelPosition.All || wheel.position == driveWheels)
                {
                    wheel.collider.motorTorque = direction.y * maxAcceleration;
                }
            }
        }
        
        public void OnMove(InputValue value)
        {
            direction = value.Get<Vector2>();
        }
    }
}