using System;
using System.Collections.Generic;
using UnityEngine;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{

  public class SoViewerBundle : ScriptableObject, IToSource<IViewerBundle>
  {
    public List<SoViewerLayout> items;
    public List<ViewCloudMono> linkedCloud;

    [SerializeField] private ClassTypeReference objType;

    public string ViewObjName
    {
      get => GetRef?.TypeName();
    }

    public IViewerBundle GetRef
    {
      get => objType != null ? (IViewerBundle)Activator.CreateInstance(objType.Type) : null;
    }

    public void SetRef(IViewerBundle obj)
    {
      objType = new ClassTypeReference(obj.GetType());
      items = new List<SoViewerLayout>();
      if (obj.layouts.Valid())
        foreach (var l in obj.layouts)
        {
          var so = CreateInstance<SoViewerLayout>();
          so.SetRef(l as ViewerLayout);
          items.Add(so);
        }
    }
  }
}