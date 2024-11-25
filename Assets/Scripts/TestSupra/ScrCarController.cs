using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ScrCarController : MonoBehaviour
{
    public ScrWheel[] wheels;

    [Header("Car Specs")]
    public float wheelBase; // in meters , �չ��� �߽ɰ� �޹��� �߽� ������ �Ÿ�
    public float rearTrack; // in meters , �޹��� �߽� ������ �Ÿ�
    public float turnRadius; // in meters , ������ Ŀ�긦 �� ��, ������ �߽��� ���� ���� ������

    [Header("Inputs")]
    public float steerInput;

    public float ackermannAngleLeft;
    public float ackermannAngleRight;

    void Update()
    {
        AckermannSteering();
        ApplySteering();
    }

    void AckermannSteering() // ��Ŀ�� ��Ƽ� ���� ����
    {
        steerInput = Input.GetAxis("Horizontal");

        if (steerInput > 0) // ���������� ȸ��
        {
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        }
        else if (steerInput < 0) // �������� ȸ��
        {
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
        }
        else
        {
            ackermannAngleLeft = 0;
            ackermannAngleRight = 0;
        }
    }

    void ApplySteering() // ������ ��Ŀ�� ��Ƽ� ����
    {
        foreach (ScrWheel w in wheels)
        {
            if (w.wheelFrontLeft)
            {
                w.steerAngle = ackermannAngleLeft;
            }
            if (w.wheelFrontRight)
            {
                w.steerAngle = ackermannAngleRight;
            }
        }
    }
}
