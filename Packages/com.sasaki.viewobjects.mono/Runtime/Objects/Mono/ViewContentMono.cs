using System;
using System.Collections.Generic;
using UnityEngine;
using ViewTo.ViewObject;
using Object = UnityEngine.Object;

namespace ViewTo.Connector.Unity
{
  public class ViewContentMono : ViewObjMono<ViewContent>
  {
    [SerializeField] private SoViewContent data;

    public int ContentMask
    {
      get { return data != null ? data.mask : 0; }
    }
    
    public ViewContent GetRef
    {
      get => data != null ? data.GetRef : null;
    }

    public List<GameObject> GetSceneObjs
    {
      get => transform.GatherKids();
      set
      {
        foreach (Transform child in transform)
          MonoHelper.SafeDestroy(child.gameObject);

        // StoreSceneObjs(value);
      }
    }

    public ViewColor ViewColor
    {
      get => data.viewColor;
      set => data.viewColor = value;
    }

    public string ViewName
    {
      get => data != null ? data.viewName : "empty";
    }

    public void SetArgs(ViewContent value)
    {
      var newData = ScriptableObject.CreateInstance<SoViewContent>();
      newData.SetRef(value);
      newData.viewName = data.viewName;

      data = newData;

      gameObject.name = data.FullName;
    }

    protected override void ImportValidObj(ViewContent viewObj)
    {
      data = ScriptableObject.CreateInstance<SoViewContent>();
      data.SetRef(viewObj);

      gameObject.name = data.FullName;
      SetMeshData(data.objects);
    }

    /// <summary>
    ///   references the objects converted to the view content list and imports them
    /// </summary>
    private void SetMeshData(List<Object> items)
    {
      if (!items.Valid()) return;

      var mat = new Material(data.analysisMaterial);

      foreach (var obj in items)
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

        var meshRend = go.GetComponent<MeshRenderer>();
        if (meshRend == null)
          meshRend = go.AddComponent<MeshRenderer>();

        meshRend.material = Instantiate(mat);

        go.transform.SetParent(transform);
      }
    }

  }
}