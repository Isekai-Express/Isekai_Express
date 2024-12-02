using System;
using UnityEngine;
using UnityEngine.AI;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private Transform _target = null;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _stoppingDistance = 0.5f;
 
    private NavMeshAgent _navAgent = null;
    private Animator _animator = null;
    
    private void Start()
    {
        // 멤버변수 초기화
        _navAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _navAgent.speed = 10f;
        _navAgent.angularSpeed = 400f;
        _navAgent.acceleration = 40f;
    }

    private void Update()
    {
        Vector3 targetPosition = new Vector3(_target.position.x, transform.position.y, _target.position.z);
        float distance = Vector3.Distance(transform.position, targetPosition);
        _navAgent.SetDestination(targetPosition);

        if (distance > _stoppingDistance)
        {
            _navAgent.isStopped = false;
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _navAgent.isStopped = true;
            _animator.SetBool("isMoving", false);
        }

        if (_navAgent.velocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_navAgent.velocity);
            transform.rotation = targetRotation;
        }
    }
}
