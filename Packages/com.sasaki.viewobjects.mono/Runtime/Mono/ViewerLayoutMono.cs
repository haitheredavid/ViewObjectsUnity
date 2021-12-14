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

    [SerializeField] private ClassTypeReference objType;

    public IViewerLayout GetRef
    {
      get => objType != null ? (ViewerLayout)Activator.CreateInstance(objType.Type) : null;
    }

    public List<IViewer> viewers { get; private set; }

    public void SetRef(IViewerLayout obj)
    {
      objType = new ClassTypeReference(obj.GetType());
    }

    public void Clear()
    {
      if (viewers.Valid())
        ViewObjMonoExt.ClearList(sceneViewers);

      sceneViewers = new List<ViewerMono>();
    }

    public void Init(SoViewerLayout input)
    {
      Clear();
      data = input;
      gameObject.name = input.GetName;
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