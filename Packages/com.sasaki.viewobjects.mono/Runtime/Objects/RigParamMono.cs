using UnityEngine;
using ViewTo.Objects;
using ViewTo.Objects.Structure;

namespace ViewTo.Connector.Unity
{
  public class RigParamMono : ViewObjBehaviour<RigParameters>
  {

    
    protected override void ImportValidObj()
    {
      name = viewObj.TypeName();
      foreach (var b in viewObj.bundles)
      {
        if (b == null) continue;

        var mono = b.ToViewMono();
      }
    }
  }
}