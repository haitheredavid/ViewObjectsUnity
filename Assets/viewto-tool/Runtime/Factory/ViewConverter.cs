using System.Collections.Generic;
using System.Linq;
using ViewTo.Objects;

namespace ViewTo.Connector.Unity
{
  public static partial class ViewConverter
  {

    public static List<CloudInfo> ToView(this IEnumerable<CloudShell> obj)
    {
      return obj.Select(i => new CloudInfo
                          {id = i.id, count = i.count}).ToList();
    }

    public static List<CloudShell> ToUnity(this Dictionary<string, CloudPoint[]> obj)
    {
      return obj.Select(i => new CloudShell
        {
          count = i.Value.Length,
          id = i.Key,
          points = i.Value.ToUnity(out var m),
          meta = m
        })
        .ToList();
    }

    public static RigMono ToUnity(this RigObj obj, bool importIfValid = true) => obj.ToUnity<RigMono>(importIfValid);

  }
}