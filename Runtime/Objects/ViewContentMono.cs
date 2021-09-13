﻿using System;
using System.Collections.Generic;
using UnityEngine;
using ViewTo.StudyObject;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{
  public class ViewContentMono : ViewObjBehaviour<ViewContent>
  {

    [SerializeField] private Color32 viewColor = Color.magenta;
    [SerializeField] private int contentMask;
    [SerializeField] private Material analysisMaterial;
    [SerializeField] private int objectCount;
    [SerializeField] private string viewName = "test name";

    public List<ViewerBundle> bundles
    {
      get
      {
        if (viewObj is TargetContent tc)
          return tc.bundles;

        Debug.Log("Only Target Content will have Viewer Bundles!");
        return null;
      }
    }

    public List<GameObject> GetSceneObjs
    {
      get => transform.GatherKids();
      set
      {
        foreach (Transform child in transform)
          MonoHelper.SafeDestroy(child.gameObject);

        StoreSceneObjs(value);
        SetMeshData();
      }
    }

    private void StoreSceneObjs(List<GameObject> items)
    {
      viewObj.objects = new List<object>();
      foreach (var obj in items)
        viewObj.objects.Add(obj);

      objectCount = viewObj.objects.Count;
    }

    public int ContentMask
    {
      get
      {
        contentMask = MaskByType();
        return contentMask;
      }
    }

    public ViewColor ViewColor
    {
      get => viewObj.viewColor;
      set
      {
        viewObj.viewColor = value;
        viewColor = value.ToUnity();
      }
    }

    public string ViewName
    {
      get => viewName;
      private set
      {
        viewName = value;
        viewObj.viewName = value;
        gameObject.name = viewObj.TypeName() + "-" + viewObj.viewName;
      }
    }

    public void SetArgs(ViewContent args)
    {
      viewObj = args;
      ViewName = args.viewName;

      StoreSceneObjs(GetSceneObjs);
    }

    public void Params(bool args)
    {
      if (viewObj is TargetContent tc)
        tc.isolate = args;
    }

    public void Params(string args)
    {
      ViewName = args;
    }

    protected override void ImportValidObj()
    {
      ViewName = viewObj.viewName;
      SetMeshData();
    }

    /// <summary>
    ///   references the objects converted to the view content list and imports them
    /// </summary>
    private void SetMeshData()
    {
      if (!viewObj.objects.Valid()) return;

      foreach (var obj in viewObj.objects)
      {
        var mat = analysisMaterial != null ? new Material(analysisMaterial) : new Material(Shader.Find("Unlit"));

        GameObject go;
        if (obj is GameObject o)
          go = o;
        else if (obj is Mesh mesh)
        {
          go = new GameObject(mesh.name);
          var filter = new GameObject().AddComponent<MeshFilter>();

          // TODO move to shared toolkit
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

        meshRend.material = mat;

        go.transform.SetParent(transform);
      }
    }

    private int MaskByType() => viewObj switch
    {
      DesignContent _ => 6,
      TargetContent _ => 7,
      BlockerContent _ => 8,
      _ => 0
    };

  }
}