using System;
using UnityEngine;
using ViewTo.StudyObject;

namespace ViewTo.Connector.Unity
{

  [ExecuteAlways]
  public abstract class CloudMono<TObj> : ViewObjMono<TObj>, IGenerateID where TObj : ViewCloud, new()
  {

    [SerializeField] private string id;
    [SerializeField] private CloudPoint[] points;

    public string viewID
    {
      get => id;
    }

    public int pointCount
    {
      get => points.Valid() ? points.Length : 0;
    }

    public Vector3[] GetPoints()
    {
      return!points.Valid() ? null : points.ToUnity();
    }

    public void SetPoints(CloudPoint[] pts)
    {
      if (!pts.Valid()) return;

      points = pts;
    }

    protected override void ImportValidObj(TObj viewObj)
    {
      if (viewObj is ViewCloud vo)
      {
        gameObject.name = vo.TypeName();
        id = vo.viewID.Valid() ? vo.viewID : Guid.NewGuid().ToString();
        SetPoints(viewObj.points);
      }
    }
    
    
  }

}