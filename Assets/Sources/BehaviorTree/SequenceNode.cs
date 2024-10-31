using System.Collections.Generic;

/// <summary>
/// 자식 노드 중 하나라도 Failure 이면 Failure 반환, Running이나 Success라면 둘 중 하나 반환 (Running 우선도가 높음)
/// </summary>
public sealed class SequenceNode : INode
{
    List<INode> _childs;

    public SequenceNode(List<INode> childs)
    {
        _childs = childs;
    }

    public INode.ENodeState Evaluate()
    {
        if (_childs == null || _childs.Count == 0)
            return INode.ENodeState.ENS_Failure;

        foreach (var child in _childs)
        {
            switch(child.Evaluate())
            {
                case INode.ENodeState.ENS_Running:
                    return INode.ENodeState.ENS_Running;
                case INode.ENodeState.ENS_Success:
                    continue;
                case INode.ENodeState.ENS_Failure:
                    return INode.ENodeState.ENS_Failure;
            }
        }

        return INode.ENodeState.ENS_Success;
    }
}