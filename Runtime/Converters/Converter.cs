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

    public static ViewContentMono ToViewMono(this ViewContent obj) => obj.ToViewMono<ViewContentMono>();
    
    public static ViewerBundleMono ToViewMono(this ViewerBundle obj) => obj.ToViewMono<ViewerBundleMono>();
    public static ViewerLayoutMono ToViewMono(this ViewerLayout obj) => obj.ToViewMono<ViewerLayoutMono>();
  
    public static TShell ToViewMono<TShell>(this ViewObj obj) where TShell : ViewObjMono => new GameObject().ToViewMono<TShell>(obj);

    public static TShell ToViewMono<TShell>(this GameObject go, ViewObj obj) where TShell : ViewObjMono
    {
      var shell = (TShell)go.AddComponent(typeof(TShell));
      shell.TryImport(obj);
      return shell;
    }

    public static ViewObjMono ToViewMono(this ViewObj obj)
    {
      return obj switch
      {
        ViewStudy o => o.ToViewMono(),
        ContentBundle o => o.ToViewMono(),

        ViewerBundle o => o.ToViewMono(),
        ViewerLayout o => o.ToViewMono(),

        ResultCloud o => o.ToViewMono(),
        ViewCloud o => o.ToViewMono(),

        ViewContent o => o.ToViewMono(),
        _ => throw new ArgumentOutOfRangeException(nameof(obj), obj, null)
      };

    }
  }
}