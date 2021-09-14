using System.Collections.Generic;
using UnityEngine;
using ViewTo.StudyObject;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{

  public class ViewerBundleMono : ViewObjBehaviour<ViewerBundle>
  {

    [SerializeField] private Texture2D colorStrip;
    [SerializeField] private List<Color32> colors;
    [SerializeField] private List<ViewerLayoutMono> layouts = new List<ViewerLayoutMono>();
    public List<CloudShell> linkedShell { get; private set; }

    public bool linked
    {
      get => linkedShell != null && linkedShell.Count != 0;
    }

    public void Clear()
    {
      MonoHelper.ClearList(layouts);
      layouts = new List<ViewerLayoutMono>();
    }

    public void Build()
    {
      if (!layouts.Valid())
        return;

      foreach (var l in layouts)
        l.Build();
      
      
    }

    protected override void ImportValidObj(ViewerBundle viewObj)
    {
      gameObject.name = viewObj.TypeName();

      layouts = new List<ViewerLayoutMono>();

      if (!viewObj.layouts.Valid()) return;

      foreach (var iLayout in viewObj.layouts)
      {
        if (iLayout is ViewerLayout vl)
        {
          LayoutToScene(vl);
        }
      }

      if (viewObj is ViewerBundleLinked link && link.linkedClouds.Valid())
        linkedShell = link.linkedClouds;
    }

    private void LayoutToScene(ViewerLayout obj)
    {
      var mono = obj.ToViewMono();
      mono.transform.SetParent(transform);
      layouts.Add(mono);
    }

  }
}