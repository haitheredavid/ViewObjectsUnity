using System;
using System.Collections.Generic;
using UnityEngine;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{
  public class ViewContentMono : ViewObjMono<ViewContent>
  {
    [SerializeField] private SoViewContent data;
    [SerializeField] private List<ContentObj> objs;

    public int contentLayerMask
    {
      get { return data != null ? data.mask : 0; }
    }

    public ViewContent GetRef
    {
      get => data != null ? data.GetRef : null;
    }

    public ViewColor ViewColor
    {
      get => data.viewColor;
      set
      {
        data.viewColor = value;
        var c = value.ToUnity();
        if (objs.Valid())
          foreach (var o in objs)
            o.MatColor = c;
      }
    }

    public string ViewName
    {
      get => data != null ? data.viewName : "empty";
    }

    protected override void ImportValidObj(ViewContent viewObj)
    {
      data = ScriptableObject.CreateInstance<SoViewContent>();
      data.SetRef(viewObj);

      gameObject.name = data.FullName;
    }

    /// <summary>
    ///   references the objects converted to the view content list and imports them
    /// </summary>
    public void PrimeMeshData(Action<ContentObj> onAfterPrime = null)
    {
      if (!data.objects.Valid()) return;

      objs = new List<ContentObj>();


      foreach (var obj in data.objects)
      {
        GameObject go;
        if (obj is GameObject o)
          go = o;
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
          throw new Exception("not an object set for converting");

        go.transform.SetParent(transform);
        var contentObj = go.AddComponent<ContentObj>();

        contentObj.SetParent(this, new Material(data.analysisMaterial));
        objs.Add(contentObj);
        
        gameObject.SetLayerRecursively(contentLayerMask);
        
        onAfterPrime?.Invoke(contentObj);
      }
    }

  }
}