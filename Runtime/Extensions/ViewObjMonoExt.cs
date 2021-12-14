using ViewTo.Connector.Unity;

namespace ViewTo.Objects.Mono.Extensions
{
  public static partial class ViewObjMonoExt
  {

    public static int GetCount(this IViewCloud obj) => obj.points.Valid() ? obj.points.Length : 0;

    public static string FullName(this ViewContentMono obj) => obj.GetRef?.TypeName()[0] + "-" + obj.viewName;
  }
}