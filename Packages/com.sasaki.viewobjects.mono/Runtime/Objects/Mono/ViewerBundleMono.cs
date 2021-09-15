using System.Collections.Generic;
using UnityEngine;
using ViewTo.StudyObject;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{

  public class ViewerBundleMono : ViewObjMono<ViewerBundle>
  {

    [SerializeField] private List<ViewerLayoutMono> layouts = new List<ViewerLayoutMono>();

 
    public void Clear()
    {
      MonoHelper.ClearList(layouts);
      layouts = new List<ViewerLayoutMono>();
    }

    public void Build()
    {
      if (!layouts.Valid())
        return;

      foreach (var l in layouts)
        l.Build();
    }

    
    
    protected override void ImportValidObj(ViewerBundle viewObj)
    {
      gameObject.name = viewObj.TypeName();

      layouts = new List<ViewerLayoutMono>();

      if (!viewObj.layouts.Valid()) return;

      foreach (var iLayout in viewObj.layouts)
      {
        if (iLayout is ViewerLayout vl)
        {
          LayoutToScene(vl);
        }
      }

    }

    private void LayoutToScene(ViewerLayout obj)
    {
      var mono = obj.ToViewMono();
      mono.transform.SetParent(transform);
      layouts.Add(mono);
    }

  }
}