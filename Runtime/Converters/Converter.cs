using System;
using UnityEngine;
using ViewTo.AnalysisObject;
using ViewTo.StudyObject;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{
  public static partial class ViewConverter
  {
    public static ViewCloudMono ToViewMono(this ViewCloud obj) => obj.ToViewMono<ViewCloudMono>();
    public static ResultCloudMono ToViewMono(this ResultCloud obj) => obj.ToViewMono<ResultCloudMono>();

    public static ViewStudyMono ToViewMono(this ViewStudy obj) => obj.ToViewMono<ViewStudyMono>();
    public static ContentBundleMono ToViewMono(this ContentBundle obj) => obj.ToViewMono<ContentBundleMono>();

    public static RigMono ToViewMono(this Rig obj) => obj.ToViewMono<RigMono>();
    public static ViewerBundleMono ToViewMono(this ViewerBundle obj) => obj.ToViewMono<ViewerBundleMono>();
    public static ViewerLayoutMono ToViewMono(this ViewerLayout obj) => obj.ToViewMono<ViewerLayoutMono>();

    public static TargetByTypeContentMono ToViewMono(this TargetContent obj) => obj.ToViewMono<TargetByTypeContentMono>();
    public static BlockerByTypeContentMono ToViewMono(this BlockerContent obj) => obj.ToViewMono<BlockerByTypeContentMono>();
    public static DesignByTypeContentMono ToViewMono(this DesignContent obj) => obj.ToViewMono<DesignByTypeContentMono>();

    public static TShell ToViewMono<TShell>(this ViewObj obj) where TShell : ViewObjBehaviour => new GameObject().ToViewMono<TShell>(obj);

    public static TShell ToViewMono<TShell>(this GameObject go, ViewObj obj) where TShell : ViewObjBehaviour
    {
      var shell = (TShell)go.AddComponent(typeof(TShell));
      shell.TryImport(obj);
      return shell;
    }

    public static ViewObjBehaviour ToViewMono(this ViewObj obj)
    {
      return obj switch
      {
        ViewStudy o => o.ToViewMono(),
        ContentBundle o => o.ToViewMono(),

        Rig o => null,
        // RigParameters o => null,
        ViewerBundle o => o.ToViewMono(),
        ViewerLayout o => o.ToViewMono(),

        ResultCloud o => o.ToViewMono(),
        ViewCloud o => o.ToViewMono(),

        TargetContent o => o.ToViewMono(),
        DesignContent o => o.ToViewMono(),
        BlockerContent o => o.ToViewMono(),

        _ => throw new ArgumentOutOfRangeException(nameof(obj), obj, null)
      };

    }
  }
}