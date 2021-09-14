using UnityEditor;
using UnityEngine.UIElements;
using ViewTo.Connector.Unity;

namespace ViewTo.Objects.Mono
{

  [CustomEditor(typeof(ViewerLayoutMono))]
  public class ViewerLayoutEditor : Editor
  {
    private ViewerLayoutMono mono;
    private VisualTreeAsset visualTree;

    private void OnEnable()
    {
      visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(MonoHelper.StylesPath + "ViewerLayoutDoc.uxml");
    }

    public override VisualElement CreateInspectorGUI()
    {
      mono = (ViewerLayoutMono)target;
      var root = visualTree.CloneTree();

      var layoutDropDown = root.Q<ViewerLayoutDropDownElement>();
      layoutDropDown.SetValue(mono.GetRefType);

      layoutDropDown.onViewObjUpdate += (sender, args) =>
      {
        var data = serializedObject.FindProperty("data").objectReferenceValue as SoViewerLayout;
        
        var storedName = string.Empty;
        if (data != null)
          storedName = data.viewName;

        data.SetRef(args.viewObj);
        data.viewName = storedName;

        serializedObject.FindProperty("data").objectReferenceValue = data;
        serializedObject.ApplyModifiedProperties();
      };

      return root;
    }
  }
}