using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyVision))]
public class EnemyVisionEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyVision fov = (EnemyVision)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);
        Vector3 viewAngleA = fov.directionFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.directionFromAngle(fov.viewAngle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

        foreach (Transform visibleTarget in fov.visibleTargets)
        {
            Handles.color = Color.red;
            Handles.DrawLine(fov.transform.position, visibleTarget.position);
        }
    }
}
