using UnityEngine;

namespace ViewTo.Objects.Mono.Extensions
{
  public static partial class ViewObjMonoExt
  {
    
    public static ViewColor ToView(this Color32 value, int index)
    {
      return new ViewColor(value.r, value.g, value.b, value.a, index);
    }

    public static Color32 ToUnity(this ViewColor value)
    {
      return value != null ? new Color32(value.R, value.G, value.B, value.A) : default;
    }
  }
}