using System;
using UnityEngine;
using ViewTo.ViewObject;

namespace ViewTo.Objects.Mono
{

  public class SoViewerLayout : ScriptableObject
  {
    [SerializeField] private ClassTypeReference objType;

    public IViewerLayout GetRef
    {
      get => objType != null ? (IViewerLayout)Activator.CreateInstance(objType.Type) : null;
    }
    
    public void SetRef(IViewerLayout obj)
    {
      objType = new ClassTypeReference(obj.GetType());
    }
    
    public string GetName
    {
      get => GetRef?.TypeName();
    }

    public ViewerLayoutMono ToViewMono()
    {
      var mono = new GameObject().AddComponent<ViewerLayoutMono>();
      mono.SetData(this);
      return mono;
    }
  }

}