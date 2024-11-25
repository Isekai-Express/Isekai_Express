using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 positionOffset = new Vector3(0, 2, -5);
    public Vector3 rotationOffset = new Vector3(10, 0, 0);

    [SerializeField] private float smoothMove = 1500;
    [SerializeField] private float smoothRotate = 150f;

    // Update is called once per frame
    void LateUpdate()
    {
        MoveCamera();
        RotateCamera();
    }

    void MoveCamera()
    {
        Vector3 position = target.TransformPoint(positionOffset); // Ÿ���� ���� ��ǥ���� Offset��ŭ ������ ���� ��ǥ�� ��ȯ�Ѵ�
        transform.position = Vector3.Lerp(transform.position, position, smoothMove * Time.deltaTime);
    }

    void RotateCamera()
    {
        Quaternion targetRotation = Quaternion.LookRotation((target.position - transform.position) + rotationOffset, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothRotate * Time.deltaTime);
    }
}
