using System.Collections.Generic;
using UnityEngine;
using ViewTo.StudyObject;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{

  public class ContentBundleMono : ViewObjBehaviour<ContentBundle>
  {

    [SerializeField] private List<ViewContentMono> contents;
    
    public List<ViewContentMono> Get<TContent>() where TContent : ViewContent
    {
      var item = new List<ViewContentMono>();
      foreach (var i in contents)
        if (i.GetRef is TContent)
          item.Add(i);

      return item;
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