using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MovingPlatform))]
[CanEditMultipleObjects]
public class MovingPlatformEditor : Editor
{
    private SerializedProperty _speed;
    private SerializedProperty _b;

    private void OnEnable()
    {
        // Setup the SerializedProperties.
        _speed = serializedObject.FindProperty("Speed");
        _b = serializedObject.FindProperty("B");
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();


        EditorGUILayout.HelpBox("Editor message", MessageType.Info);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Print A"))
        {
            Debug.Log(((MovingPlatform) target).transform.position);
        }

        if (GUILayout.Button("Print B"))
        {
            Debug.Log(((MovingPlatform) target).B);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(_b, new GUIContent("Point B"));
        EditorGUILayout.Slider(_speed, 0.1f, 10f, new GUIContent("Speed"));


        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }
}