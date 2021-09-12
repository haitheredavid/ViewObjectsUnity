using System.Collections.Generic;
using UnityEngine;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{

  public class ViewerLayoutMono : ViewObjBehaviour<ViewerLayout>
  {
    public List<ViewerMono> viewers { get; private set; }

    private ViewerMono ViewerPrefab
    {
      get => new GameObject().AddComponent<ViewerMono>();
    }

    public void Clear()
    {
      if (viewers.Valid())
        ViewMonoHelper.ClearList(viewers);

      viewers = new List<ViewerMono>();
    }

    protected override void ImportValidObj()
    {
      gameObject.name = viewObj.TypeName();
      if (!viewObj.viewers.Valid()) return;

      Clear();

      var prefab = ViewerPrefab;

      foreach (var v in viewObj.viewers)
      {
        var mono = Instantiate(prefab, transform);
        mono.Setup(v);
        viewers.Add(mono);
      }

      ViewMonoHelper.SafeDestroy(prefab.gameObject);
    }
  }
}