using System.Collections.Generic;
using UnityEngine;
using ViewTo.Objects;
using ViewTo.Objects.Structure;

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
        if (obj == null) continue;

        var mono = obj.ToViewMono();
        mono.transform.SetParent(transform);
        loadedObjs.Add(mono);
      }
    }
  }
}