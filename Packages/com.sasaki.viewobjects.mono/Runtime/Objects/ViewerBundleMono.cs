using System.Collections.Generic;
using UnityEngine;
using ViewTo.Objects;
using ViewTo.Objects.Elements;
using ViewTo.Objects.Structure;

namespace ViewTo.Connector.Unity
{

  public class ViewerBundleMono : ViewObjBehaviour<ViewerBundle>
  {

    
    [SerializeField] private bool global;
    [SerializeField] private Texture2D colorStrip;
    [SerializeField] private List<string> clouds;
    [SerializeField] private List<Color32> colors;
    [SerializeField] private List<ViewerLayoutMono> layouts;

    public List<MetaShell> linkedShell { get; private set; }

    public int viewerCount { get; private set; }
    public List<ViewerMono> viewers { get; set; }

    public bool hasLinks
    {
      get => linkedShell != null && linkedShell.Count != 0;
    }

    public bool IsGlobal
    {
      get => global;
      set => global = value;
    }

    protected override void ImportValidObj()
    {
      gameObject.name = viewObj.TypeName();
      viewerCount = 0;

      if (viewObj is ViewerBundleLinked linked && linked.linkedClouds.Valid())
        linkedShell = linked.linkedClouds;

      viewers = new List<ViewerMono>();
      
      layouts = new List<ViewerLayoutMono>();
      foreach (var l in viewObj.layouts)
      {
        viewerCount += l.viewers.Count;
        layouts.Add(l.ToViewMono());
      }
    }
  }
}