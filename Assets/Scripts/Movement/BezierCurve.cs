using System;
using UnityEditor;
using UnityEngine;


// 차후 Bezier곡선 여러개 연결되는 상황 위해 배열로 만들어줘야 할 듯
public class BezierCurve : MonoBehaviour
{
    // Test
    [SerializeField] private GameObject testObject;
    [Range(0, 1)] public float testRange;
    
    // p1 : 시작점 / p4 : 도착점
    public Vector3 _p1;
    public Vector3 _p2;
    public Vector3 _p3;
    public Vector3 _p4;

    public Vector3 Bezier(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float value)
    {
        Vector3 _vecA = Vector3.Lerp(p1, p2, value);
        Vector3 _vecB = Vector3.Lerp(p2, p3, value);
        Vector3 _vecC = Vector3.Lerp(p3, p4, value);

        Vector3 _vecD = Vector3.Lerp(_vecA, _vecB, value);
        Vector3 _vecE = Vector3.Lerp(_vecB, _vecC, value);
        
        Vector3 _vecF = Vector3.Lerp(_vecD, _vecE, value);
        
        return _vecF;
    }

    private void Update()
    {
        testObject.transform.position = Bezier(_p1, _p2, _p3, _p4, testRange);
    }
}
