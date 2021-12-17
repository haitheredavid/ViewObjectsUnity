using System;
using System.Collections.Generic;
using UnityEngine;
using ViewTo.ViewObject;

namespace ViewTo.Objects.Mono
{
  public class ViewerLayoutMono : ViewObjMono, IViewerLayout
  {
    [SerializeField] private List<ViewerMono> sceneViewers;

    [SerializeField] private SoViewerLayout data;

    public List<IViewer> viewers { get; private set; }

    public void SetData(IViewerLayout obj)
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
      if (data == null)
        return;

      Clear();

      var prefab = new GameObject().AddComponent<ViewerMono>();
      foreach (var v in sceneViewers)
      {
        var mono = Instantiate(prefab, transform);
        mono.Setup(v);
        viewers.Add(mono);

        onBuildComplete?.Invoke(mono);
      }
      ViewObjMonoExt.SafeDestroy(prefab.gameObject);
    }
  }
}