using System;
using System.Collections.Generic;

/// <summary>
/// 주어진 condition 확인 후 success면 하위 노드 실행, false면 Failure 반환
/// </summary>
public class ConditionNode : INode
{
    private INode _node;
    private Func<bool> _condition;

    ConditionNode(INode node, Func<bool> condition)
    {
        _node = node;
        _condition = condition;
    }

    public INode.ENodeState Evaluate()
    {
        bool conditionResult = _condition.Invoke();
        return conditionResult ? _node.Evaluate() : INode.ENodeState.ENS_Failure;
    }
}
