using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMovement : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 direction;

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)direction * (Time.deltaTime * speed);
    }
}
