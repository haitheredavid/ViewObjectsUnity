using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ViewTo.Objects.Elements;

namespace ViewTo.Connector.Unity
{
  public static class ViewMonoHelper
  {

    public static readonly string RuntimeDir = "Packages/com.sasaki.viewobjects.mono/Runtime/";
    public static readonly string GUIDir = RuntimeDir + "GUI/";
    public static readonly string StylesPath = GUIDir + "Styles/";

    public static ViewCloudMono TryFetchInScene(this MetaShell shell)
    {

      return Object.FindObjectsOfType<ViewCloudMono>().FirstOrDefault(o => o.viewID != null
                                                                           && shell?.objId != null
                                                                           && o.viewID.Equals(shell.objId));
    }

    public static void ClearList<TBehaviour>(List<TBehaviour> list) where TBehaviour : MonoBehaviour
    {
      for (var i = list.Count - 1; i >= 0; i--) SafeDestroy(list[i].gameObject);
    }

    public static void SafeDestroy(Object obj)
    {
      if (Application.isPlaying)
        Object.Destroy(obj);
      else
        Object.DestroyImmediate(obj);
    }
  }
}