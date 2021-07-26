using System.Collections.Generic;
using System.Linq;
using HaiThere.Utilities;
using UnityEngine;
using ViewTo.Objects;
using ViewTo.Structure;

namespace ViewTo.Connector.Unity
{

  public class ContentBundleMono : ViewObjBehaviour<ContentBundle>
  {

    [SerializeField] private List<TargetContentMono> Targets;
    [SerializeField] private List<BlockerContentMono> Blockers;
    [SerializeField] private List<DesignContentMono> Designs;

    public IEnumerable<ViewContentMono> Contents
    {
      get
      {
        var items = new List<ViewContentMono>();
        if (Targets != null) items.AddRange(Targets);
        if (Blockers != null) items.AddRange(Blockers);
        if (Designs != null) items.AddRange(Designs);
        return items;
      }
    }

    public void SetContent<TContentMono>(TContentMono content) 
      where TContentMono : ViewContentMono
    {
      switch (content)
      {
        case TargetContentMono c:
          Targets ??= new List<TargetContentMono>();
          Targets.Add(c);
          break;
        case BlockerContentMono c:
          Blockers ??= new List<BlockerContentMono>();
          Blockers.Add(c);
          break;
        case DesignContentMono c:
          Designs ??= new List<DesignContentMono>();
          Designs.Add(c);
          break;
        default:
          Debug.Log($"Type {content.TypeName()} is not supported");
          break;
      }

    }
    protected override void ImportValidObj()
    {
      PurgeAllContent();
      if (viewObj.targets != null) Targets = AddToScene<TargetContentMono>(viewObj.targets);
      if (viewObj.blockers != null) Blockers = AddToScene<BlockerContentMono>(viewObj.blockers);
      if (viewObj.designs != null) Designs = AddToScene<DesignContentMono>(viewObj.designs);

    }

 

    private void PurgeAllContent()
    {
      var c = Contents.ToArray();
      if (c.Valid())
        for (int i = c.Length - 1; i > 0; i--)
          Destroy(c[i]);

      Targets = new List<TargetContentMono>();
      Blockers = new List<BlockerContentMono>();
      Designs = new List<DesignContentMono>();

    }

    private List<TShell> AddToScene<TShell>(IEnumerable<ViewContent> objs) where TShell : ViewObjBehaviour
      => objs.Select(o => o.ToUnity<TShell>()).Where(item => item != null).ToList();



  
  }
}