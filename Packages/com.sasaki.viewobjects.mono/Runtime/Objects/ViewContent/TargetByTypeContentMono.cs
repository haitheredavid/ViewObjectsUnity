using System.Collections.Generic;
using UnityEngine;
using ViewTo.StudyObject;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{

  public class TargetByTypeContentMono : ViewByTypeContentMono<TargetContent>, IViewName
  {
    [SerializeField] private bool isolateContent;
    [SerializeField] private string viewObjectName;

    public List<ViewerBundle> bundles { get; set; }

    public bool isolate
    {
      get => isolateContent;
      set => isolateContent = value;
    }

    public string viewName
    {
      get => viewObjectName;
      set => viewObjectName = value;
    }
    //
    // public override ViewContent CopyObj()
    // {
    //   return new TargetContent {viewColor = ViewColor, bundles = bundles, viewName = viewName, isolate = isolate};
    // }

    protected override void SetValidContent(TargetContent content)
    {
      viewObjectName = content.viewName;
      gameObject.name = viewName;
      bundles = content.bundles;
      isolate = content.isolate;
    }
  }
}