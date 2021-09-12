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

    [SerializeField] protected int pointCount;

    public string viewID
    {
      get
      {
        if (!viewObj.viewID.Valid())
          viewObj.viewID = Guid.NewGuid().ToString();

        return viewObj.viewID;
      }
    }

    public CloudPoint[] Points
    {
      get
      {
        viewObj.points ??= Array.Empty<CloudPoint>();
        return viewObj.points;
      }
      set
      {
        if (!value.Valid())
          return;

        viewObj.points = value;
        pointCount = value.Length;
        TriggerImportArgs(CreateArgs);
      }
    }

    private CloudImportArgs CreateArgs
    {
      get =>
        new CloudImportArgs(
          (from p in Points select p.ToUnity()).ToList(),
          (from p in Points select Color.white).Select(dummy => (Color32)dummy).ToList());
    }

    protected override void ImportValidObj()
    {
      pointCount = Points.Valid() ? Points.Length : 0;
      gameObject.name = viewObj.TypeName();

      TriggerImportArgs(CreateArgs);
    }
  }

}