using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    public float _followSpeed = 1.0f;
    public Vector3 _springArmVector = Vector3.zero;

    private Transform _camTransform;
    private Transform _targetTransform; 
    
    private void Start()
    {
        _camTransform = GetComponent<Transform>();
        _targetTransform = _target.GetComponent<Transform>();
        _springArmVector = _camTransform.position - _targetTransform.position;
    }

    private void Update()
    {
        Vector3 nextPosition = _targetTransform.position + _springArmVector;
        _camTransform.position = Vector3.Lerp(_camTransform.position, nextPosition, _followSpeed * Time.deltaTime);
    }
}
