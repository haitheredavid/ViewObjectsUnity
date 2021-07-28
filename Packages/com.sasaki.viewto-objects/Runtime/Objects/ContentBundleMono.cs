using System.Collections.Generic;
using HaiThere.Utilities;
using UnityEngine;
using ViewTo.Objects;

namespace ViewTo.Connector.Unity
{

  public class ContentBundleMono : ViewObjBehaviour<ContentBundle>
  {

    // TODO compile lists added during edit mode  
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

    public List<ViewContentMono> GetAll => contents.Valid() ? contents : new List<ViewContentMono>();

    public List<TContent> Get<TContent>() where TContent : ViewContentMono
    {
      var item = new List<TContent>();
      foreach (var i in contents)
        if (i is TContent casted)
          item.Add(casted);

      return item;
    }

    public void Set(ViewContent item)
    {
      contents ??= new List<ViewContentMono>();
      contents.Add(item.ToUnity());

      switch (item)
      {
        case TargetContent o:
          targets.Add(o);
          break;
        case BlockerContent o:
          blockers.Add(o);
          break;
        case DesignContent o:
          designs.Add(o);
          break;
      }
    }
    
    public override ContentBundle CopyObj()
    {
      return new ContentBundle
      {
        targets = viewObj.targets, blockers = viewObj.blockers, designs = viewObj.designs
      };
    }

    protected override void ImportValidObj()
    {
      Purge();

      var objs = new List<ViewContent>();

      if (targets.Valid()) objs.AddRange(targets);
      if (blockers.Valid()) objs.AddRange(blockers);
      if (designs.Valid()) objs.AddRange(designs);

      contents = new List<ViewContentMono>();

      foreach (var item in objs)
        contents.Add(item.ToUnity());
    }

    private void Purge()
    {
      if (contents.Valid())
        for (var i = contents.Count - 1; i > 0; i--)
          Destroy(contents[i]);
    }
  }
}