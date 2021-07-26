using UnityEngine;
using ViewTo.Objects;

namespace ViewTo.Connector.Unity
{

  public static partial class ViewConverter
  {

    public static Vector3[] ToUnity(this CloudPoint[] value)
    {
      var points = new Vector3[value.Length];
      for (var i = 0; i < value.Length; i++)
        points[i] = value[i].ToUnity();

      return points;
    }

    /// <summary>
    ///   flips value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Vector3 ToUnity(this CloudPoint value) => new Vector3((float)value.x, (float)value.z, (float)value.y);
  }
}