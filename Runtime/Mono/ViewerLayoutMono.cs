﻿using System;
using System.Collections.Generic;
using UnityEngine;
using ViewObjects;
using ViewObjects.Viewer;

namespace ViewTo.Objects.Mono
{
  public class ViewerLayoutMono : ViewObjMono, IViewerLayout
  {
    [SerializeField] private List<ViewerMono> sceneViewers;

    [SerializeField] private SoViewerLayout data;

    public List<IViewer> viewers
    {
      get
      {
        var res = new List<IViewer>();
        if (data != null)
        {
          var layout = data.GetRef;
          if (layout != default)
          {
            foreach (var viewer in layout.viewers)
            {
              res.Add(viewer);
            }
          }
        }
        return res;
      }
    }

    public void SetData(ViewerLayout obj)
    {
      Clear();

      data = ScriptableObject.CreateInstance<SoViewerLayout>();
      data.SetRef(obj);

      gameObject.name = data.GetName;
    }

    public void SetData(SoViewerLayout obj)
    {
      Clear();

      data = obj;
      gameObject.name = data.GetName;
    }

    public void Clear()
    {
      if (viewers.Valid())
        ViewObjMonoExt.ClearList(sceneViewers);

      sceneViewers = new List<ViewerMono>();
    }

    public void Build(Action<ViewerMono> onBuildComplete = null)
    {
      Debug.Log($"Build Process called for {name}");
      
      if (data == null)
      {
        Debug.LogWarning($"{name} does not have valid viewer layout data to build");
        return;
      }

      Clear();

      var prefab = new GameObject().AddComponent<ViewerMono>();
      foreach (var v in viewers)
      {
        var mono = Instantiate(prefab, transform);
        mono.Setup(v);
        sceneViewers.Add(mono);

        onBuildComplete?.Invoke(mono);
      }
      ViewObjMonoExt.SafeDestroy(prefab.gameObject);
    }
  }
}