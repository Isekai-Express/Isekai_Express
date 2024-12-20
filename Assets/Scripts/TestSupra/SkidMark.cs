using UnityEngine;

public class SkidMark : MonoBehaviour
{
    public GameObject wheelEffect;
    TrailRenderer tr;

    // Update is called once per frame
    void Start()
    {
        tr = GetComponentInChildren<TrailRenderer>();
    }

    void Update()
    {
        skidMark();
    }

    void skidMark() // 임시 브레이크키 shift
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            tr.emitting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            tr.emitting = false;
        }
    }
}
