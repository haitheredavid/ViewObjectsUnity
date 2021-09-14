using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ViewTo.ViewObject;
using Object = UnityEngine.Object;

namespace ViewTo.Connector.Unity
{

  public class SoViewContent : ScriptableObject, IToSource<ViewContent>
  {

    public int mask;
    public string viewName;
    public List<Object> objects;
    
    public Material analysisMaterial;

    public int colorId;
    public Color32 color;

    public ViewColor viewColor
    {
      get { return new ViewColor(color.r, color.g, color.b, color.a, colorId); }
      set
      {
        color = value.ToUnity();
        colorId = value.Id;
      }
    }

    public string FullName
    {
      get => GetRef?.TypeName()[0] + "-" + viewName;
    }
    
    public ClassTypeReference objType;
    public ViewContent GetRef
    {
      get => objType != null ? (ViewContent)Activator.CreateInstance(objType.Type) : null;
    }

    public void SetRef(ViewContent obj)
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