using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace ViewTo.Objects
{
  public static partial class ViewObjMonoExt
  {

    public static List<ViewColor> CreateBundledColors(this ICollection content)
    {
      var colorSet = new HashSet<ViewColor>();
      var r = new Random();

      while (colorSet.Count < content.Count)
      {
        var b = new byte[3];
        r.NextBytes(b);
        var tempColor = new ViewColor(b[0], b[1], b[2], 255, colorSet.Count);
        colorSet.Add(tempColor);
      }
      return colorSet.ToList();
    }
    
    public static ViewColor ToView(this Color32 value, int index) => new ViewColor(value.r, value.g, value.b, value.a, index);

    public static Color32 ToUnity(this ViewColor value) => value != null ? new Color32(value.R, value.G, value.B, value.A) : default;
  }
}