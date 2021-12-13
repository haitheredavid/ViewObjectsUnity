using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ViewTo.Objects.Mono.Extensions;
using Object = UnityEngine.Object;

namespace ViewTo.Connector.Unity
{
  public abstract class ViewContentMono : ViewObjMono
  {
    [SerializeField] private string contentName;

    [SerializeField] [HideInInspector] private int mask;

    [SerializeField] private SoViewContent data;

    [SerializeField] private List<Object> contentObjs;

    [SerializeField] private Material analysisMaterial;

    [SerializeField] private int colorId;
    [SerializeField] private Color32 color;

    [SerializeField] private ClassTypeReference objType;

    public List<object> objects
    {
      get => contentObjs.Valid() ? contentObjs.Cast<object>().ToList() : new List<object>();
      set
      {
        contentObjs = new List<Object>();

        foreach (var o in value)
          if (o is Object go)
            objects.Add(go);

      }
    }

    public IViewContent GetRef
    {
      get => objType != null ? (IViewContent)Activator.CreateInstance(objType.Type) : null;
    }

    public int contentLayerMask
    {
      get => mask;
    }

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

    public string viewName
    {
      get => contentName;
      set
      {
        contentName = value;
        gameObject.name = this.FullName();
      }
    }

    public ViewColor ViewColor
    {
      get => data.viewColor;
      set
      {
        data.viewColor = value;
        var c = value.ToUnity();
        if (contentObjs.Valid())
          foreach (var o in contentObjs)
            if (o is ContentObj contentObj)
              contentObj.MatColor = c;
      }
    }

    public void ImportValidObj(IViewContent obj)
    {
      analysisMaterial = new Material(Shader.Find(@"Unlit/Color"));
      objType = new ClassTypeReference(obj.GetType());

      viewName = obj.viewName;
      viewColor = obj.viewColor;
    }

    /// <summary>
    ///   references the objects converted to the view content list and imports them
    /// </summary>
    public void PrimeMeshData(Action<ContentObj> onAfterPrime = null)
    {
      if (!data.objects.Valid()) return;


      foreach (var obj in data.objects)
      {
        GameObject go;
        if (obj is GameObject o)
        {
          go = o;
        }
        else if (obj is Mesh mesh)
        {
          go = new GameObject(mesh.name);
          var filter = go.AddComponent<MeshFilter>();

          if (Application.isPlaying)
            filter.mesh = mesh;
          else
            filter.sharedMesh = mesh;
        }
        else
        {
          throw new Exception("no objects set for converting");
        }

        go.transform.SetParent(transform);
        var contentObj = go.AddComponent<ContentObj>();

        contentObj.SetParent(this, new Material(data.analysisMaterial));

        gameObject.SetLayerRecursively(contentLayerMask);

        onAfterPrime?.Invoke(contentObj);
      }
    }
  }
}