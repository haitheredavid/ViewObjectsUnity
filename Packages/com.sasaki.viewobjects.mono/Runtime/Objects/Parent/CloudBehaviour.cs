using System;
using System.Linq;
using UnityEngine;
using ViewTo.Objects.Mono.Args;
using ViewTo.StudyObject;

namespace ViewTo.Connector.Unity
{

  [ExecuteAlways]
  public abstract class CloudBehaviour<TObj> : ViewObjBehaviour<TObj> where TObj : ViewCloud, new()
  {

    [SerializeField] private string viewId;
    [SerializeField] private CloudPoint[] points;

    public string GetId
    {
      get => viewId;
    }

    public int pointCount
    {
      get => points.Valid() ? points.Length : 0;
    }

    private CloudImportArgs CreateArgs
    {
      get =>
        new CloudImportArgs(
          (from p in points select p.ToUnity()).ToList(),
          (from p in points select Color.white).Select(dummy => (Color32)dummy).ToList());
    }

    public void SetPoints(CloudPoint[] pts)
    {
      if (!pts.Valid()) return;

      points = pts;
      TriggerImportArgs(CreateArgs);
    }

    protected override void ImportValidObj(TObj viewObj)
    {
      if (viewObj is ViewCloud vo)
      {
        gameObject.name = vo.TypeName();
        viewId = vo.viewID.Valid() ? vo.viewID : Guid.NewGuid().ToString();
        SetPoints(viewObj.points);


      }
    }
  }

}