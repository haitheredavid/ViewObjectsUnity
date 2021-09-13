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
      layoutDropDown.onViewObjUpdate += (sender, args) => mono.TryImport(args.viewObj);
      layoutDropDown.SetValue(mono.viewObj);

      return root;
    }
  }
}