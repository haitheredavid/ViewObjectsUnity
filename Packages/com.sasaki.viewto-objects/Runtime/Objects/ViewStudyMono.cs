using System.Collections.Generic;
using HaiThere.Utilities;
using UnityEngine;
using ViewTo.Objects;

namespace ViewTo.Connector.Unity
{
  public class ViewStudyMono : ViewObjBehaviour<ViewStudy>
  {

    [SerializeField] private string viewName;
    [SerializeField] private List<ViewObjBehaviour> objs;

    public List<ViewObj> ViewObjs
    {
      get => viewObj.objs.Valid() ? viewObj.objs : new List<ViewObj>();
      set => viewObj.objs = value;
    }

    public override ViewStudy CopyObj()
    {
      return new ViewStudy
        {objs = viewObj.objs, viewName = viewObj.viewName};
    }
    protected override void ImportValidObj()
    {
      viewName = viewObj.viewName;
    }
  }
}