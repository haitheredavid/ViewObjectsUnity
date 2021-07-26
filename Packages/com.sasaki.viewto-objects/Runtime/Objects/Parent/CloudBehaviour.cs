using System;
using System.Collections.Generic;
using System.Linq;
using Pcx;
using UnityEngine;
using ViewTo.Objects;

namespace ViewTo.Connector.Unity
{

  [ExecuteAlways]
  public abstract class CloudBehaviour<TObj> : ViewObjBehaviour<TObj> where TObj : ViewCloud
  {

    [SerializeField] protected int pointCount = 0;
    [SerializeField] private PointCloudRenderer cloudRenderer;
    private CloudPoint[] _cloudPoints;


    public CloudPoint[] Points
    {
      get => _cloudPoints;
    }

    protected override void ImportValidObj()
    {
      _cloudPoints = viewObj.points;
      pointCount = _cloudPoints.Length;
      RenderPoints((from p in _cloudPoints select p.ToUnity()).ToList());
    }

    
    protected void RenderPoints(List<Vector3> points)
    {
      var colors = (from p in points select Color.white).Select(dummy => (Color32)dummy).ToList();
      RenderPoints(points, colors);
    }
    

    protected void RenderPoints(List<Vector3> points, List<Color32> colors)
    {
      var data = ScriptableObject.CreateInstance<PointCloudData>();
      data.Initialize(points, colors);

      if (cloudRenderer == null)
      {
        cloudRenderer = new GameObject("CloudRender").AddComponent<PointCloudRenderer>();
        cloudRenderer.gameObject.transform.SetParent(transform);
      }

      cloudRenderer.sourceData = data;
    }

   

  }

}