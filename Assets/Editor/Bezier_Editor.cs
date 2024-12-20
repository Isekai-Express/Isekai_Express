using UnityEditor;
using UnityEngine;

namespace Editors
{
    // BezierCurve 클래스에 대한 Custom Editor
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BezierCurve))]
    public class Bezier_Editor : UnityEditor.Editor
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
}