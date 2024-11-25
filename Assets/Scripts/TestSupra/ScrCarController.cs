using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ScrCarController : MonoBehaviour
{
    public ScrWheel[] wheels;

    [Header("Car Specs")]
    public float wheelBase; // in meters , 앞바퀴 중심과 뒷바퀴 중심 사이의 거리
    public float rearTrack; // in meters , 뒷바퀴 중심 사이의 거리
    public float turnRadius; // in meters , 차량이 커브를 돌 때, 차량의 중심이 도는 원의 반지름

    [Header("Inputs")]
    public float steerInput;

    public float ackermannAngleLeft;
    public float ackermannAngleRight;

    void Update()
    {
        AckermannSteering();
        ApplySteering();
    }

    void AckermannSteering() // 아커만 스티어링 공식 적용
    {
        steerInput = Input.GetAxis("Horizontal");

        if (steerInput > 0) // 오른쪽으로 회전
        {
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        }
        else if (steerInput < 0) // 왼쪽으로 회전
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

    void ApplySteering() // 바퀴에 아커만 스티어링 적용
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
