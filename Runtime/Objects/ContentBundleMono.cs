using System.Collections.Generic;
using UnityEngine;
using ViewTo.Objects;
using ViewTo.Objects.Structure;

namespace ViewTo.Connector.Unity
{

  public class ContentBundleMono : ViewObjBehaviour<ContentBundle>
  {

    [SerializeField] private List<ViewByTypeContentMono> contents;

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

    public List<ViewByTypeContentMono> GetAll
    {
      get => contents.Valid() ? contents : new List<ViewByTypeContentMono>();
    }

    public void Set(IEnumerable<ViewByTypeContentMono> items)
    {
      foreach (var i in items)
        if (i != null)
          Set(i);
    }

    public void Set(ViewByTypeContentMono item)
    {
      contents ??= new List<ViewByTypeContentMono>();
      item.transform.SetParent(transform);
      contents.Add(item);
    }

    public List<TContent> Get<TContent>() where TContent : ViewByTypeContentMono
    {
      var item = new List<TContent>();
      foreach (var i in contents)
        if (i is TContent casted)
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

      contents = new List<ViewByTypeContentMono>();
      foreach (var i in items)
        if (i.ToViewMono() is ViewByTypeContentMono mono)
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

      contents = new List<ViewByTypeContentMono>();

    }
  }
}