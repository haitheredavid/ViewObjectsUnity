using UnityEditor;
using UnityEngine.UIElements;
using ViewTo.Connector.Unity;

namespace ViewTo.Objects.Mono
{
  [CustomEditor(typeof(ViewCloudMono))]
  public class ViewContentEditor : Editor
  {
    private ViewContentMono mono;
    private VisualTreeAsset visualTree;

    private void OnEnable()
    {
      visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ViewMonoHelper.StylesPath + "ViewContentDoc.uxml");
    }

    public override VisualElement CreateInspectorGUI()
    {
      mono = (ViewContentMono)target;
      var root = visualTree.CloneTree();

      var layoutDropDown = root.Q<ViewContentDropDownElement>();
      layoutDropDown.onViewObjUpdate += (sender, args) => mono.TryImport(args.viewObj);
      layoutDropDown.SetValue(mono.viewObj);

      return root;
    }
  }
}