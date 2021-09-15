using System;
using System.Collections.Generic;
using UnityEngine;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{
  public class SoViewerLayout : ScriptableObject, IToSource<ViewerLayout>
  {

    [SerializeField] private ClassTypeReference objType;

    public string GetName
    {
      get => GetRef?.TypeName();
    }

    public List<Viewer> viewers
    {
      get => GetRef?.viewers;
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