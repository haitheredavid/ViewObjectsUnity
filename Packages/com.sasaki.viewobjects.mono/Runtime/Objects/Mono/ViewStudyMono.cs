using System.Collections.Generic;
using UnityEngine;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{
  public class ViewStudyMono : ViewObjMono, IViewStudy
  {

    [SerializeField] private List<ViewObjMono> loadedObjs;

    public string viewName
    {
      get => gameObject.name;
      set => name = value;
    }
    public bool isValid
    {
      get => objs.Valid() && viewName.Valid();
    }

    public List<object> objs
    {
      get
      {
        var res = new List<object>();
        foreach (var obj in loadedObjs)
        {
          if (obj != null)
            res.Add(obj);
        }

        return res;
      }
      set
      {
        loadedObjs = new List<ViewObjMono>();

        foreach (var obj in value)
          if (obj is ViewObjMono mono)
            loadedObjs.Add(mono);
      }
    }

    public override void TryImport(ViewObj @object)
    {
      if (@object is ViewStudy viewObj)
      {
        viewName = viewObj.viewName;
        gameObject.name = viewName.Valid() ? viewName : viewObj.TypeName();

        if (!viewObj.objs.Valid()) return;

        loadedObjs = new List<ViewObjMono>();
        foreach (var obj in viewObj.objs)
        {
          if (obj is ViewObj vo)
          {
            var mono = vo.ToViewMono();
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

  }
}