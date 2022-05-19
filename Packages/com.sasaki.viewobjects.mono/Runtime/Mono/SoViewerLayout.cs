using System;
using UnityEngine;
using ViewObjects;
using ViewObjects.Viewer;

namespace ViewTo.Objects.Mono
{

  public class SoViewerLayout : ScriptableObject
  {
    [SerializeField] private ClassTypeReference objType;

    public IViewerLayout GetRef
    {
      get => objType != null ? (ViewerLayout)Activator.CreateInstance(objType.Type) : null;
    }
    
    public void SetRef(ViewerLayout obj)
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