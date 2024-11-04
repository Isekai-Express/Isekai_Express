using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMovement : MonoBehaviour
{
    public Vector2 speed = new Vector2(5, 1);

    private Vector2 direction;

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }
    
    private void FixedUpdate()
    {
        transform.position += (Vector3)(direction * speed) * Time.deltaTime  ;
    }
}
