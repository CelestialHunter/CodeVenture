using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BinaryBitwiseScript)), CanEditMultipleObjects]
public class BinaryBitwiseEditor : Editor
{

    public SerializedProperty
        input1_prop,
        input2_prop,
        output_prop,
        operationDisplay_prop,
        selectedOperation_prop,
        operationNames_prop;

    private void OnEnable()
    {
        input1_prop = serializedObject.FindProperty("input1");
        input2_prop = serializedObject.FindProperty("input2");
        output_prop = serializedObject.FindProperty("output");
        operationDisplay_prop = serializedObject.FindProperty("operationDisplay");
        selectedOperation_prop = serializedObject.FindProperty("selectedOperation");
        operationNames_prop = serializedObject.FindProperty("operationNames");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        EditorGUILayout.PropertyField(selectedOperation_prop);
        EditorGUILayout.PropertyField(input1_prop);
        EditorGUILayout.PropertyField(input2_prop);
        EditorGUILayout.PropertyField(output_prop);
        EditorGUILayout.PropertyField(operationDisplay_prop);

        if (input1_prop.objectReferenceValue == null || input2_prop.objectReferenceValue == null || output_prop.objectReferenceValue == null)
        {
            EditorGUILayout.HelpBox("Please assign all inputs and outputs", MessageType.Warning);
            ((TMPro.TMP_Text)operationDisplay_prop.objectReferenceValue).text = "NOP";
        }
        else ((TMPro.TMP_Text)operationDisplay_prop.objectReferenceValue).text = BinaryBitwiseScript.operationNames[selectedOperation_prop.enumValueIndex];

        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
