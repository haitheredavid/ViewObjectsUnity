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
      get { return GetRef?.TypeName(); }
    }
    
    
    public List<Viewer> viewers
    {
      get
      {
        var o = Activator.CreateInstance(objType.Type) as ViewerLayout;
        return o?.viewers;
      }
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