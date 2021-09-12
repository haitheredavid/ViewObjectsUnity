using System.Collections.Generic;
using UnityEngine;
using ViewTo.StudyObject;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{

  public class ViewerBundleMono : ViewObjBehaviour<ViewerBundle>
  {
    [SerializeField] private bool global;
    [SerializeField] private Texture2D colorStrip;
    [SerializeField] private List<string> clouds;
    [SerializeField] private List<Color32> colors;
    public List<ViewerLayoutMono> layouts = new List<ViewerLayoutMono>();

    public ViewerLayout cachedLayout = new ViewerLayout();

    public List<CloudShell> linkedShell { get; private set; }

    public bool hasLinks
    {
      get => linkedShell != null && linkedShell.Count != 0;
    }
    public bool IsGlobal
    {
      get => global;
      set => global = value;
    }

    public void AddLayout()
    {
      layouts ??= new List<ViewerLayoutMono>();
      LayoutToScene(cachedLayout);
      cachedLayout = new ViewerLayout();
    }

    public void Clear()
    {
      ViewMonoHelper.ClearList(layouts);
      layouts = new List<ViewerLayoutMono>();
    }

    protected override void ImportValidObj()
    {
      gameObject.name = viewObj.TypeName();

      if (viewObj is ViewerBundleLinked linked && linked.linkedClouds.Valid())
        linkedShell = linked.linkedClouds;

      layouts = new List<ViewerLayoutMono>();
      if (!viewObj.layouts.Valid()) return;

      foreach (var l in viewObj.layouts)
        if (l is ViewerLayout vl)
          LayoutToScene(vl);
    }

    private void LayoutToScene(ViewerLayout obj)
    {
      var mono = obj.ToViewMono();
      mono.transform.SetParent(transform);
      layouts.Add(mono);
    }

    public void SetParams(ViewerLayout viewerLayout)
    {
      Debug.Log("Add param button clicked");
      cachedLayout = viewerLayout;
    }
  }
}