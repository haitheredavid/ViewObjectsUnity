using System.Collections.Generic;
using System.Linq;

namespace ViewTo.Connector.Unity
{
  public static partial class ViewConverter
  {

    public static List<CloudShellUnity> ToUnity(this Dictionary<string, CloudPoint[]> obj) => obj.Select(i => new CloudShellUnity
      {
        count = i.Value.Length,
        id = i.Key,
        points = i.Value.ToUnity(out var m),
        meta = m
      })
      .ToList();
  }
}