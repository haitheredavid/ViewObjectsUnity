using System;
using UnityEditor;
using ViewTo.Connector.Unity;

namespace ViewTo.Objects.Mono
{
  [CustomEditor(typeof(RigMono))]
  public class RigMonoInspector : Editor
  {
    private SerializedProperty @params;

    // private void OnEnable()
    // {
    //   @params = serializedObject.FindProperty("rigParams");
    //   
    // }

  }
}