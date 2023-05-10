using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    Waypoint Waypoint => target as Waypoint;
    private void OnSceneGUI()
    {
        Handles.color = Color.cyan;
        for (int i = 0; i < Waypoint.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            //Create Handle
            Vector3 currentWaypointPoint = Waypoint.CurrentPosition + Waypoint.Points[i];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint,
            Quaternion.identity, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

            //Create Text
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 16;
            textStyle.normal.textColor = Color.white;
            Vector3 textAlligment = Vector3.down * 0.35f + Vector3.right * 0.35f;
            Handles.Label(Waypoint.CurrentPosition + Waypoint.Points[i] + textAlligment,
            $"{i + 1}", textStyle);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObjects(new Object[] { target }, "Free Move Handle");
                Waypoint.Points[i] = newWaypointPoint - Waypoint.CurrentPosition;
            }
        }

    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

    }
}

