using TreeEditor;
using UnityEngine;

public class ScrWheel : MonoBehaviour
{

    // 질량 중심을 (0,-0.5,0)에서 (0,-0.2,0)으로 올렸더니 차가 들리는 문제 해결
    // Angular Drag 값 조정을 통해 자연스러운 커브 구현

    private Rigidbody rb;

    public bool wheelFrontLeft;
    public bool wheelFrontRight;
    public bool wheelRearLeft;
    public bool wheelRearRight;

    [Header("Suspension")]
    public float restLength; // 스프링의 기본 길이
    public float springTravel; // 스프링이 변하는 길이
    public float springStiffness; // 스프링 강성
    public float damperStiffness; // 댐퍼 강성

    private float springMinLength; // 스프링의 최소 길이
    private float springMaxLength; // 스프링의 최대 길이
    private float springLastLength; // 스프링의 전 프레임의 길이
    private float springLength; // 현재 스프링의 길이
    private float springForce; // 스프링의 힘
    private float springVelocity; // 스프링의 속도
    private float damperForce; // damper : 자동차의 완충기? 그 힘

    private Vector3 suspensionForce;

    [Header("Wheel")]
    public float wheelRadius;
    private float wheelAngle; // 현재의 바퀴의 각도

    public float steerAngle; // 회전할 바퀴의 각도
    public float steerTime = 10f; // 회전하는데 걸리는 시간

    private Vector3 wheelVelocityLS; // Local Space
    public float forwardForce = 1500f;
    public float sideForce = 500f;
    private float forceX;
    private float forceY;

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();

        springMinLength = restLength - springTravel;
        springMaxLength = restLength + springTravel;
    }

    void Update()
    {
        wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, steerTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);

        Debug.DrawRay(transform.position, -Vector3.up * (springLength + wheelRadius), Color.green);
    }

    void FixedUpdate()
    {     
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, springMaxLength + wheelRadius)) // 지면과 닿으면
        {
            #region 서스펜션 계산

            springLastLength = springLength;
            springLength = hit.distance - wheelRadius;
            springLength = Mathf.Clamp(springLength, springMinLength, springMaxLength);
            springVelocity = (springLastLength - springLength) / Time.fixedDeltaTime; // 스프링의 속도 물리학적 계산
            springForce = springStiffness * (restLength - springLength);
            damperForce = damperStiffness * springVelocity;

            suspensionForce = (springForce + damperForce) * transform.up; // raycast의 반대방향

            #endregion

            wheelVelocityLS = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point)); // 바퀴에 힘을 가하는 point의 local space 속도 계산
            forceX = Input.GetAxis("Vertical") * forwardForce;
            forceY = wheelVelocityLS.x * sideForce;

            if (forceX < 0) // 후진
            {
                rb.AddForceAtPosition((suspensionForce + (transform.forward * forceX) + (-transform.right * forceY)), hit.point);
            }
            else // 자동으로 전진
            {
                rb.AddForceAtPosition((suspensionForce + (transform.forward * forwardForce) + (-transform.right * forceY)), hit.point);
            }
        }
    }
}
