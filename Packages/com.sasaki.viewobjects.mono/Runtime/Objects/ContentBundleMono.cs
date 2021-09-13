using System.Collections.Generic;
using UnityEngine;
using ViewTo.StudyObject;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{

  public class ContentBundleMono : ViewObjBehaviour<ContentBundle>
  {

    [SerializeField] private List<ViewContentMono> contents;

    public List<TargetContent> targets
    {
      get => viewObj.targets;
      private set => viewObj.targets = value;
    }

    public List<BlockerContent> blockers
    {
      get => viewObj.blockers;
      private set => viewObj.blockers = value;
    }

    public List<DesignContent> designs
    {
      get => viewObj.designs;
      private set => viewObj.designs = value;
    }

    public List<ViewContentMono> GetAll
    {
      get => contents.Valid() ? contents : new List<ViewContentMono>();
    }

    public void Set(IEnumerable<ViewContentMono> items)
    {
      foreach (var i in items)
        if (i != null)
          Set(i);
    }

    public void Set(ViewContentMono item)
    {
      contents ??= new List<ViewContentMono>();
      item.transform.SetParent(transform);
      contents.Add(item);
    }

    public List<TContent> Get<TContent>() where TContent : ViewContent
    {
      var item = new List<TContent>();
      foreach (var i in contents)
        if (i.viewObj is TContent casted)
          item.Add(casted);

      return item;
    }

    protected override void ImportValidObj()
    {

      gameObject.name = "Content Bundle";
      var items = new List<ViewContent>();

      if (targets.Valid())
        items.AddRange(targets);
      if (blockers.Valid())
        items.AddRange(blockers);
      if (designs.Valid())
        items.AddRange(designs);

      contents = new List<ViewContentMono>();
      foreach (var i in items)
        if (i.ToViewMono() is ViewContentMono mono)
        {
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