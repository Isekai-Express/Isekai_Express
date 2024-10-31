/// <summary>
/// Node 인터페이스, Node는 Running, Success, Failure 3가지 상태를 가진다
/// 노드의 상태를 평가하기 위한 Evaluate 함수 존재
/// </summary>
public interface INode
{
    public enum ENodeState
    {
        ENS_Running,
        ENS_Success,
        ENS_Failure,
    }

    public ENodeState Evaluate();
}
