#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class DropDownStringAttribute : PropertyAttribute
{

  public DropDownStringAttribute(string value) => Name = value;
  public string Name { get; }
}

public class ReadOnlyAttribute : PropertyAttribute
{ }

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{

  public override void OnGUI(
    Rect position,
    SerializedProperty property,
    GUIContent label
  )
  {
    GUI.enabled = false;
    EditorGUI.PropertyField(position, property, label, true);
    GUI.enabled = true;
  }
}

#endif