using System.Collections.Generic;
using UnityEngine;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{
  public class ViewStudyMono : ViewObjMono<ViewStudy>
  {

    [SerializeField] private string viewName;
    [SerializeField] private List<ViewObjMono> loadedObjs;

    public List<ViewObjMono> objs
    {
      get => loadedObjs;
      set => loadedObjs = value;
    }

    public string ViewName
    {
      get => viewName;
      set
      {
        viewName = value;
        name = value;
      }
    }

    protected override void ImportValidObj(ViewStudy viewObj)
    {
      viewName = viewObj.viewName;
      gameObject.name = viewName.Valid() ? viewName : viewObj.TypeName();

      if (!viewObj.objs.Valid()) return;


      loadedObjs = new List<ViewObjMono>();
      foreach (var obj in viewObj.objs)
      {
        var mono = obj.ToViewMono();
        if (mono == null)
        {
          Debug.Log($"did not convert {obj.TypeName()} to mono ");
          continue;
        }
        mono.transform.SetParent(transform);
        loadedObjs.Add(mono);
      }
    }
  }
}