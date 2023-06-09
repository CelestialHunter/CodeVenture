using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;


namespace Assets.Editor
{
    [CustomEditor(typeof(DoorScript)), CanEditMultipleObjects]
    public class DoorScriptEditor : UnityEditor.Editor
    {
        public SerializedProperty
            roomName_prop,
            isOpen_prop,
            isLocked_prop,
            _isInteractable_prop,
            door_prop,
            roomNumberText_prop;

        public void OnEnable()
        {
            roomName_prop = serializedObject.FindProperty("roomName");
            isOpen_prop = serializedObject.FindProperty("isOpen");
            isLocked_prop = serializedObject.FindProperty("isLocked");
            _isInteractable_prop = serializedObject.FindProperty("_isInteractable");
            door_prop = serializedObject.FindProperty("door");
            roomNumberText_prop = serializedObject.FindProperty("roomNumberText");
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            EditorGUILayout.PropertyField(roomName_prop);
            EditorGUILayout.PropertyField(isOpen_prop);
            EditorGUILayout.PropertyField(isLocked_prop);
            EditorGUILayout.PropertyField(_isInteractable_prop);
            EditorGUILayout.PropertyField(door_prop);
            EditorGUILayout.PropertyField(roomNumberText_prop);

            if (door_prop.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("Please assign a door", MessageType.Warning);
                return;
            }

            if (roomNumberText_prop.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("Please assign a room number text", MessageType.Warning);
                return;
            }

            if (isOpen_prop.boolValue)
            {
                ((Transform)door_prop.objectReferenceValue).localRotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                ((Transform)door_prop.objectReferenceValue).localRotation = Quaternion.Euler(0, 0, 0);
            }

            ((TMP_Text)roomNumberText_prop.objectReferenceValue).text = roomName_prop.stringValue;
            roomNumberText_prop.serializedObject.ApplyModifiedProperties();
            PrefabUtility.RecordPrefabInstancePropertyModifications(((Transform)door_prop.objectReferenceValue).parent.gameObject);

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
}
