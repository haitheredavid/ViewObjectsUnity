using System.Collections.Generic;
using UnityEngine;

namespace ViewTo.Objects.Mono.Args
{
  public class CloudImportArgs : ViewObjArgs
  {
    public readonly List<Color32> colors;
    public readonly List<Vector3> points;

    public CloudImportArgs(List<Vector3> points, List<Color32> colors)
    {
      this.points = points;
      this.colors = colors;
    }
  }
}