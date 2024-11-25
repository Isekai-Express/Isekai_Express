using TreeEditor;
using UnityEngine;

public class ScrWheel : MonoBehaviour
{

    // ���� �߽��� (0,-0.5,0)���� (0,-0.2,0)���� �÷ȴ��� ���� �鸮�� ���� �ذ�
    // Angular Drag �� ������ ���� �ڿ������� Ŀ�� ����

    private Rigidbody rb;

    public bool wheelFrontLeft;
    public bool wheelFrontRight;
    public bool wheelRearLeft;
    public bool wheelRearRight;

    [Header("Suspension")]
    public float restLength; // �������� �⺻ ����
    public float springTravel; // �������� ���ϴ� ����
    public float springStiffness; // ������ ����
    public float damperStiffness; // ���� ����

    private float springMinLength; // �������� �ּ� ����
    private float springMaxLength; // �������� �ִ� ����
    private float springLastLength; // �������� �� �������� ����
    private float springLength; // ���� �������� ����
    private float springForce; // �������� ��
    private float springVelocity; // �������� �ӵ�
    private float damperForce; // damper : �ڵ����� �����? �� ��

    private Vector3 suspensionForce;

    [Header("Wheel")]
    public float wheelRadius;
    private float wheelAngle; // ������ ������ ����

    public float steerAngle; // ȸ���� ������ ����
    public float steerTime = 10f; // ȸ���ϴµ� �ɸ��� �ð�

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
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, springMaxLength + wheelRadius)) // ����� ������
        {
            #region ������� ���

            springLastLength = springLength;
            springLength = hit.distance - wheelRadius;
            springLength = Mathf.Clamp(springLength, springMinLength, springMaxLength);
            springVelocity = (springLastLength - springLength) / Time.fixedDeltaTime; // �������� �ӵ� �������� ���
            springForce = springStiffness * (restLength - springLength);
            damperForce = damperStiffness * springVelocity;

            suspensionForce = (springForce + damperForce) * transform.up; // raycast�� �ݴ����

            #endregion

            wheelVelocityLS = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point)); // ������ ���� ���ϴ� point�� local space �ӵ� ���
            forceX = Input.GetAxis("Vertical") * forwardForce;
            forceY = wheelVelocityLS.x * sideForce;

            if (forceX < 0) // ����
            {
                rb.AddForceAtPosition((suspensionForce + (transform.forward * forceX) + (-transform.right * forceY)), hit.point);
            }
            else // �ڵ����� ����
            {
                rb.AddForceAtPosition((suspensionForce + (transform.forward * forwardForce) + (-transform.right * forceY)), hit.point);
            }
        }
    }
}
