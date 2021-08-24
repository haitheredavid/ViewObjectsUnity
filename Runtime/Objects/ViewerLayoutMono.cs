using System.Collections.Generic;
using UnityEngine;
using ViewTo.Objects.Structure;

namespace ViewTo.Connector.Unity
{
  public class ViewerLayoutMono : ViewObjBehaviour<ViewerLayout>
  {

    private ViewerMono _viewerPrefab => new GameObject().AddComponent<ViewerMono>();

    public List<ViewerMono> viewers { get; private set; }

    protected override void ImportValidObj()
    {
      gameObject.name = viewObj.TypeName();
      
      if (!viewObj.viewers.Valid()) return;

      viewers = new List<ViewerMono>();
      var prefab = _viewerPrefab;
      foreach (var v in viewObj.viewers)
      {
        var mono = Instantiate(prefab, transform);
        mono.Setup(v);
        viewers.Add(mono);
      }
    }
  }
}