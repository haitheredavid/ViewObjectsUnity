using System;
using System.Collections.Generic;
using UnityEngine;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{
  public class ViewerLayoutMono : ViewObjMono<ViewerLayout>
  {

    [SerializeField] private SoViewerLayout data;
    
    public List<ViewerMono> viewers { get; private set; }
    
    public ViewerLayout GetRefType
    {
      get { return data != null ? data.GetRef : null; }
    }

    public void Clear()
    {
      if (viewers.Valid())
        MonoHelper.ClearList(viewers);

      viewers = new List<ViewerMono>();
    }
    
    public void Init(SoViewerLayout so)
    {
      Clear();
      
      data = so;
      gameObject.name = so.GetName;
    }

    public void Build(Action<ViewerMono> onBuildComplete = null)
    {
      if (data == null)
        return;

      Clear();

      var prefab = new GameObject().AddComponent<ViewerMono>();
      foreach (var v in data.viewers)
      {
        var mono = Instantiate(prefab, transform);
        mono.Setup(v);
        viewers.Add(mono);

        onBuildComplete?.Invoke(mono);
      }
      MonoHelper.SafeDestroy(prefab.gameObject);
    }

    protected override void ImportValidObj(ViewerLayout viewObj)
    {
      data = ScriptableObject.CreateInstance<SoViewerLayout>();
      data.SetRef(viewObj);

      data.name = viewObj.TypeName();
      gameObject.name = viewObj.TypeName();
    }
  }
}