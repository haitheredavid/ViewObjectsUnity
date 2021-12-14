using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ViewTo.Objects.Mono
{

  public class ContentBundleMono : ViewObjMono, IViewContentBundle
  {
    [SerializeField] private List<ViewContentMono> viewContents;

    public List<IViewContent> contents
    {
      get => viewContents.Valid() ? viewContents.Cast<IViewContent>().ToList() : new List<IViewContent>();
      set
      {
        viewContents = new List<ViewContentMono>();
        foreach (var v in value)
        {
          ViewContentMono mono = null;
          if (v is ViewContentMono contentMono)
          {
            mono = contentMono;
          }
          else
          {
            mono = new GameObject().AddComponent<ViewContentMono>();
            mono.ImportValidObj(v);
          }
          mono.transform.SetParent(transform);
          viewContents.Add(mono);
        }
      }
    }

    public void ChangeColors()
    {
      var colors = contents.CreateBundledColors();
      for (var i = 0; i < contents.Count; i++)
        contents[i].viewColor = colors[i];
    }

    public void Prime(Action<ViewContentMono> OnAfterPrime = null, Action<ContentObj> OnContentObjPrimed = null)
    {
      foreach (var c in viewContents)
      {
        c.PrimeMeshData(OnContentObjPrimed);
        OnAfterPrime?.Invoke(c);
      }
    }

    private void Purge()
    {
      if (viewContents.Valid())
        for (var i = contents.Count - 1; i >= 0; i--)
          if (Application.isPlaying)
            Destroy(viewContents[i].gameObject);
          else
            DestroyImmediate(viewContents[i].gameObject);

      viewContents = new List<ViewContentMono>();

    }
  }
}