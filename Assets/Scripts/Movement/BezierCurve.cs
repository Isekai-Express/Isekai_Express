using System;
using UnityEditor;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    // Test용 GameObject와 float
    public GameObject GameObject;
    [Range(0, 1)] public float Test;
    
    public Vector3 _p1;
    public Vector3 _p2;
    public Vector3 _p3;
    public Vector3 _p4;

    // Test용 Update문
    private void Update()
    {
        GameObject.transform.position = Bezier(_p1, _p2, _p3, _p4, Test);
    }

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
}

[CanEditMultipleObjects]
[CustomEditor(typeof(BezierCurve))]
public class Bezier_Editor : Editor
{
    private void OnSceneGUI()
    {
        BezierCurve Generator = (BezierCurve)target;
        
        // 포지션 조작할 수 있게 만듬 -> Gizmo 생성
        Generator._p1 = Handles.PositionHandle(Generator._p1, Quaternion.identity);
        Generator._p2 = Handles.PositionHandle(Generator._p2, Quaternion.identity);
        Generator._p3 = Handles.PositionHandle(Generator._p3, Quaternion.identity);
        Generator._p4 = Handles.PositionHandle(Generator._p4, Quaternion.identity);
        
        // Handle 직선 생성 (p1-p2, p3-p4 잇는 직선)
        Handles.DrawLine(Generator._p1, Generator._p2);
        Handles.DrawLine(Generator._p3, Generator._p4);
        
        // Bezier 곡선 생성
        for (int i = 0; i < 100; i++)
        {
            Vector3 Before, After;
            Before = Generator.Bezier(Generator._p1, Generator._p2, Generator._p3, Generator._p4, i * 0.01f);
            After = Generator.Bezier(Generator._p1, Generator._p2, Generator._p3, Generator._p4, (i+1) * 0.01f);
            Handles.DrawLine(Before, After);
        }
    }
}