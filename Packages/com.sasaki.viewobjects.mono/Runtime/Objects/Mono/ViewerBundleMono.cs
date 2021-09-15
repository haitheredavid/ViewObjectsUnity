using System;
using System.Collections.Generic;
using UnityEngine;
using ViewTo.StudyObject;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{

  public class ViewerBundleMono : ViewObjMono<ViewerBundle>
  {

    [SerializeField] private SoViewerBundle data;
    [SerializeField] private List<ViewerLayoutMono> layouts = new List<ViewerLayoutMono>();

    public void Clear()
    {
      MonoHelper.ClearList(layouts);
      layouts = new List<ViewerLayoutMono>();
    }

    public void CreateViewers(Action<ViewerMono> onViewerCreate = null)
    {
      if (!layouts.Valid())
        return;

      foreach (var l in layouts)
        l.Build(onViewerCreate);
    }

    public void Init(SoViewerBundle input)
    {
      data = input;
      gameObject.name = data.ViewObjName;
      LayoutToScene();
    }

    protected override void ImportValidObj(ViewerBundle viewObj)
    {
      var input = ScriptableObject.CreateInstance<SoViewerBundle>();
      input.SetRef(viewObj);
      Init(input);
    }

    private void LayoutToScene()
    {
      if (!data.items.Valid()) return;

      layouts = new List<ViewerLayoutMono>();

      foreach (var item in data.items)
      {
        var mono = item.ToViewMono();
        mono.transform.SetParent(transform);
        layouts.Add(mono);
      }

    }

  }
}