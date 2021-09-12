using CodiceApp.EventTracking;
using UnityEditor;
using UnityEngine.UIElements;
using ViewTo.Connector.Unity;

namespace ViewTo.Objects.Mono
{
  [CustomEditor(typeof(ViewContentMono))]
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

      var element = root.Q<ViewContentDropDownElement>();
      element.SetValue(mono.viewObj);

      element.onViewObjUpdate += (sender, args) => { mono.SetArgs(args.viewObj); };
      
      element.IsolateUpdated += (sender, args) => { mono.Params(args); };
      
      return root;
    }
  }
}