using System;

/// <summary>
/// 뭔가 뭔가 이상함 이거 아닌듯
/// </summary>
public class LoopNode : INode
{
    private INode _node;
    private int _loopCount;
    private int _currentLoop;

    public LoopNode(int loopCount = 1)
    {
        _loopCount = loopCount;
    }
    
    public INode.ENodeState Evaluate()
    {
        if (_node == null)
            return INode.ENodeState.ENS_Failure;

        _currentLoop = 0;
        while (_currentLoop > _loopCount)
        {
            _currentLoop++;
            return _node.Evaluate();
        }

        return INode.ENodeState.ENS_Success;
    }
}
