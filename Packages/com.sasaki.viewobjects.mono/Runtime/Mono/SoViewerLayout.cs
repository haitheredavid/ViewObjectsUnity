using System;
using UnityEngine;
using ViewTo.ViewObject;

namespace ViewTo.Objects.Mono
{

  public class SoViewerLayout : ScriptableObject
  {

    [SerializeField] private ClassTypeReference objType;

    public string GetName
    {
      get => GetRef?.TypeName();
    }

    public ViewerLayout GetRef
    {
      get => objType != null ? (ViewerLayout)Activator.CreateInstance(objType.Type) : null;
    }

    public void SetRef(ViewerLayout obj)
    {
      objType = new ClassTypeReference(obj.GetType());
    }
  }

}