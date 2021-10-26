using System;
using System.Collections.Generic;
using UnityEngine;
using ViewTo.StudyObject;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{

  public class IiI : MonoBehaviour
  {

    public void Start()
    {
      throw new NotImplementedException();
    }

  }
  
  

  public class ContentBundleMono : ViewObjMono<ContentBundle>
  {
    [SerializeField] private List<ViewContentMono> contents;

    public void ChangeColors()
    {
      var colors = contents.CreateBundledColors();
      for (var i = 0; i < contents.Count; i++)
        contents[i].ViewColor = colors[i];
    }

    public List<ViewContentMono> GetAll()
    {
      return contents;
    }

    public List<ViewContentMono> Get<TContent>() where TContent : ViewContent
    {
      var item = new List<ViewContentMono>();
      foreach (var i in contents)
        if (i.GetRef is TContent)
          item.Add(i);

      return item;
    }

    public void Prime(Action<ViewContentMono> OnAfterPrime = null, Action<ContentObj> OnContentObjPrimed = null)
    {
      foreach (var c in contents)
      {
        c.PrimeMeshData(OnContentObjPrimed);
        OnAfterPrime?.Invoke(c);
      }
    }

    protected override void ImportValidObj(ContentBundle viewObj)
    {
      Purge();

      gameObject.name = viewObj.TypeName();

      var items = new List<ViewContent>();

      items.CheckAndAdd(viewObj.targets);
      items.CheckAndAdd(viewObj.blockers);
      items.CheckAndAdd(viewObj.designs);

      contents = new List<ViewContentMono>();
      foreach (var vc in items)
      {
        var mono = vc.ToViewMono();
        mono.transform.SetParent(transform);
        contents.Add(mono);
      }
    }

    private void Purge()
    {
      if (contents.Valid())
        for (var i = contents.Count - 1; i >= 0; i--)
          if (Application.isPlaying)
            Destroy(contents[i].gameObject);
          else
            DestroyImmediate(contents[i].gameObject);

      contents = new List<ViewContentMono>();

    }
  }
}