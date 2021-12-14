using System;
using System.Collections.Generic;
using UnityEngine;
using ViewTo.Objects.Mono.Extensions;
using ViewTo.ViewObject;
using Object = UnityEngine.Object;

namespace ViewTo.Connector.Unity
{

  public class SoViewContent : ScriptableObject, IToSource<IViewContent>
  {

    public int mask;
    public string viewName;
    public List<Object> objects;

    public Material analysisMaterial;

    public int colorId;
    public Color32 color;

    public ClassTypeReference objType;

    public ViewColor viewColor
    {
      get => new ViewColor(color.r, color.g, color.b, color.a, colorId);
      set
      {
        if (value == null)
          return;

        color = value.ToUnity();
        colorId = value.Id;
      }
    }

    public string FullName
    {
      get => GetRef?.TypeName()[0] + "-" + viewName;
    }

    public IViewContent GetRef
    {
      get => objType != null ? (IViewContent)Activator.CreateInstance(objType.Type) : null;
    }

    public void SetRef(IViewContent obj)
    {
      analysisMaterial = new Material(Shader.Find(@"Unlit/Color"));
      objType = new ClassTypeReference(obj.GetType());

      viewName = obj.viewName;
      viewColor = obj.viewColor;

      mask = obj switch
      {
        DesignContent _ => 6,
        TargetContent _ => 7,
        BlockerContent _ => 8,
        _ => 0
      };

      if (!obj.objects.Valid())
        return;


      objects = new List<Object>();
      foreach (var o in obj.objects)
        if (o is Object go)
          objects.Add(go);

    }
  }
}