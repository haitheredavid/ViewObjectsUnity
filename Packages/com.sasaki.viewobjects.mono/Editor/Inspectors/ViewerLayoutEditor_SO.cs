using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using ViewTo.Connector.Unity;

namespace ViewTo.Objects.Mono
{
  [CustomEditor(typeof(SoViewerLayout))]
  public class ViewerLayoutEditor_SO : Editor
  {
    private SoViewerLayout mono;
    private VisualTreeAsset visualTree;

    private void OnEnable()
    {
      mono = (SoViewerLayout)target;
      visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(MonoHelper.StylesPath + "ViewerLayoutDoc.uxml");
    }

    public override VisualElement CreateInspectorGUI()
    {
      var root = visualTree.CloneTree();

      var text = root.Q<TextField>("viewobj-name");
      
      text?.RegisterCallback<ChangeEvent<string>>(evt =>
      {
        Debug.Log("Changing name");
        mono.name = evt.newValue;
        EditorUtility.SetDirty(mono);
      });

      var layoutDropDown = root.Q<ViewerLayoutDropDownElement>();

      return root;
    }
  }
}