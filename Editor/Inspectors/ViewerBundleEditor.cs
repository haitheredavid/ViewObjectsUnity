using UnityEditor;
using UnityEngine.UIElements;
using ViewTo.Connector.Unity;

namespace ViewTo.Objects.Mono
{

  [CustomEditor(typeof(ViewerBundleMono))]
  public class ViewerBundleEditor : Editor
  {
    private ViewerBundleMono mono;
    private VisualTreeAsset visualTree;

    private void OnEnable()
    {
      visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ViewMonoHelper.StylesPath + "ViewerBundleDoc.uxml");
    }

    public override VisualElement CreateInspectorGUI()
    {
      mono = (ViewerBundleMono)target;
      var root = visualTree.CloneTree();

      var setupContainer = root.Q<VisualElement>("viewerlayout-setup-container");

      var createButton = setupContainer.Q<Button>("add-layout-button");
      createButton.clickable.clicked += mono.AddLayout;

      var layoutDropDown = setupContainer.Q<ViewerLayoutDropDownElement>();
      layoutDropDown.onViewObjUpdate += (sender, args) => mono.SetParams(args.viewObj);
      layoutDropDown.SetValue(mono.cachedLayout);

      var itemContainer = root.Q<VisualElement>("viewerlayout-items-container");

      var clearButton = itemContainer.Q<Button>("clear-items-button");
      clearButton.clickable.clicked += mono.Clear;

      return root;
    }
  }
}