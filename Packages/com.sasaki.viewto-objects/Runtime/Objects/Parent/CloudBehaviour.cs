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

    [SerializeField] protected int pointCount;
    [SerializeField] private PointCloudRenderer cloudRenderer;

    public CloudPoint[] Points { get; private set; }

    protected override void ImportValidObj()
    {
      Points = viewObj.points;
      pointCount = Points.Length;
      RenderPoints((from p in Points select p.ToUnity()).ToList());
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