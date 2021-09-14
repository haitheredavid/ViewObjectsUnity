using System;
using System.Collections.Generic;
using UnityEngine;
using ViewTo.StudyObject;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{
  public class SoViewerBundle : ScriptableObject, IToSource<ViewerBundle>
  {
    public List<SoViewerLayout> items;

    [SerializeField] private ClassTypeReference objType;

    public ViewerBundle RefTo
    {
      get => objType != null ? (ViewerBundle)Activator.CreateInstance(objType.Type) : null;
    }

    public void SetRef(ViewerBundle obj)
    {
      objType = new ClassTypeReference(obj.GetType());
      items = new List<SoViewerLayout>();
      foreach (var l in obj.layouts)
      {
        var so = CreateInstance<SoViewerLayout>();
        so.SetRef(l as ViewerLayout);
        items.Add(so);
      }
    }
  }
}