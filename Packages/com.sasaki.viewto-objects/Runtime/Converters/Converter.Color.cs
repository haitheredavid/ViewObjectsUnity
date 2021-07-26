using System.Collections.Generic;
using UnityEngine;
using ViewTo.Structure;

namespace ViewTo.Connector.Unity
{
  public static partial class ViewConverter
  {

    public static ViewColor ToNative(this Color32 value, int index)
    {
      return new ViewColor(value.r, value.g, value.b, value.a, index);
    }

    public static Color32 ToUnity(this ViewColor value)
    {
      return new Color32(value.R, value.G, value.B, value.A);
    }
  }
}