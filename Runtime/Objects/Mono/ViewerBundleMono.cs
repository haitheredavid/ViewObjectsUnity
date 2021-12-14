using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ViewTo.Connector.Unity
{

  public class ViewerBundleMono : ViewObjMono, IViewerBundle
  {

    [SerializeField] private SoViewerBundle data;
    [SerializeField] private List<ViewerLayoutMono> viewerLayouts = new List<ViewerLayoutMono>();

    public List<IViewerLayout> layouts
    {
      get => viewerLayouts.Valid() ? viewerLayouts.Cast<IViewerLayout>().ToList() : new List<IViewerLayout>();
      set
      {
        viewerLayouts = new List<ViewerLayoutMono>();

        foreach (var item in value)
        {
          ViewerLayoutMono mono = null;
          if (item is ViewerLayoutMono casted)
          {
            mono = casted;
          }
          else
          {
            mono = new GameObject().AddComponent<ViewerLayoutMono>();
            mono.SetRef(item);
          }

          mono.transform.SetParent(transform);
          viewerLayouts.Add(mono);
        }

      }
    }

    public void Clear()
    {
      MonoHelper.ClearList(viewerLayouts);
      viewerLayouts = new List<ViewerLayoutMono>();
    }

    public void Build(Action<ViewerMono> onViewerCreate = null)
    {
      if (!layouts.Valid())
        return;

      foreach (var l in viewerLayouts)
        l.Build(onViewerCreate);
    }
  }
}