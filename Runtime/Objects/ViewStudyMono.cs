using System.Collections.Generic;
using UnityEngine;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{
  public class ViewStudyMono : ViewObjBehaviour<ViewStudy>
  {

    [SerializeField] private string viewName;
    [SerializeField] private List<ViewObjBehaviour> loadedObjs;

    public List<ViewObjBehaviour> objs
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

    protected override void ImportValidObj()
    {
      viewName = viewObj.viewName;
      gameObject.name = viewName.Valid() ? viewName : viewObj.TypeName();

      if (!viewObj.objs.Valid()) return;


      loadedObjs = new List<ViewObjBehaviour>();
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