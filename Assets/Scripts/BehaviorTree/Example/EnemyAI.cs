using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Range")]
    [SerializeField]
    float _detectRange = 10f;
    [SerializeField]
    float _meleeAttackRange = 5f;

    [Header("Movement")]
    [SerializeField]
    float _movementSpeed = 3f;

    Vector3 _originPos = default;
    BehaviorTreeRunner _BTRunner = null;
    Animator _animator = null;

    // 이건 BlackBoard로 옮겨도 ㄱㅊ 
    Transform _detectedPlayer = null;

    const string _ATTACK_ANIM_STATE_NAME = "Attack";
    const string _ATTACK_ANIM_TRIGGER_NAME = "attack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _BTRunner = new BehaviorTreeRunner(SettingBT());

        _originPos = transform.position;
    }

    INode SettingBT()
    {
        return new SelectorNode
        (
            new List<INode>
            {
                new SequenceNode
                (
                    new List<INode>
                    {
                        new ActionNode(CheckMeleeAttacking),
                        new ActionNode(CheckEnemyWithMeleeAttackRange),
                        new ActionNode(DoMeleeAttack)
                    }
                ),
                new SequenceNode
                (
                    new List<INode>
                    {
                        new ActionNode(CheckDetectEnemy),
                        new ActionNode(MoveToDetectEnemy)
                    }
                ),
                new ActionNode(MoveToOriginPosition)
            }
        );
    }

    bool IsAnimationRunning(string stateName)
    {
        if (_animator == null) 
            return false;

        if(_animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            var normalizedTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            return normalizedTime != 0 && normalizedTime < 1f;
        }

        return false;
    }

    #region 1. 공격 노드
    INode.ENodeState CheckMeleeAttacking()
    {
        if(IsAnimationRunning(_ATTACK_ANIM_STATE_NAME))
        {
            return INode.ENodeState.ENS_Running;
        }
        return INode.ENodeState.ENS_Success;
    }

    INode.ENodeState CheckEnemyWithMeleeAttackRange()
    {
        if (_detectedPlayer == null)
            return INode.ENodeState.ENS_Failure;

        if(Vector3.SqrMagnitude(_detectedPlayer.position - transform.position) < (_meleeAttackRange * _meleeAttackRange))
        {
            return INode.ENodeState.ENS_Success;
        }

        return INode.ENodeState.ENS_Failure;
    }

    INode.ENodeState DoMeleeAttack()
    {
        if (_detectedPlayer == null)
            return INode.ENodeState.ENS_Failure;

        _animator.SetTrigger(_ATTACK_ANIM_TRIGGER_NAME);

        return INode.ENodeState.ENS_Success;
    }
    #endregion

    #region 2. 탐색 & 이동 노드
    INode.ENodeState CheckDetectEnemy()
    {
        var overlapColliders = Physics.OverlapSphere(transform.position, _detectRange, LayerMask.GetMask("Player"));

        if (overlapColliders != null && overlapColliders.Length > 0)
        {
            _detectedPlayer = overlapColliders[0].transform;
            return INode.ENodeState.ENS_Success;
        }

        _detectedPlayer = null;
        return INode.ENodeState.ENS_Failure;
    }

    INode.ENodeState MoveToDetectEnemy()
    {
        if (_detectedPlayer == null)
            return INode.ENodeState.ENS_Failure;

        if(Vector3.SqrMagnitude(_detectedPlayer.position - transform.position) < (_meleeAttackRange * _meleeAttackRange))
        {
            return INode.ENodeState.ENS_Success;
        }

        transform.position = Vector3.MoveTowards(transform.position, _detectedPlayer.position, Time.deltaTime * _movementSpeed);
        return INode.ENodeState.ENS_Running;
    }
    #endregion

    #region 3. 복귀 노드
    INode.ENodeState MoveToOriginPosition()
    {
        if(Vector3.SqrMagnitude(_originPos - transform.position) < (float.Epsilon * float.Epsilon))
        {
            return INode.ENodeState.ENS_Success;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _originPos, Time.deltaTime * _movementSpeed);
            return INode.ENodeState.ENS_Running;
        }
    }
    #endregion
}
