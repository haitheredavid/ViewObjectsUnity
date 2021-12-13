using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ViewTo.AnalysisObject;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace ViewTo.Connector.Unity
{

  public static class MonoHelper
  {

    public static readonly string RuntimeDir = "Packages/com.sasaki.viewobjects.mono/Runtime/";
    public static readonly string GUIDir = RuntimeDir + "GUI/";
    public static readonly string StylesPath = GUIDir + "Styles/";
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

    public static SoRigParam SoCreate(this IRigParam param, List<ViewColor> globalColors)
    {
      var so = ScriptableObject.CreateInstance<SoRigParam>();

      so.viewers = new List<SoViewerBundle>();
      foreach (var b in param.bundles)
      {
        var soItem = ScriptableObject.CreateInstance<SoViewerBundle>();
        soItem.SetRef(b);
        so.viewers.Add(soItem);
      }

      if (param is RigParametersIsolated iso)
      {
        so.isolate = true;
        so.contentColors = iso.colors;
      }
      else
      {
        so.isolate = false;
        so.contentColors = globalColors;
      }

      return so;
    }

    public static bool CheckForInterface<IFace>(this Type objType)
    {
      return objType.GetInterfaces().Any(x => x == typeof(IFace));
    }

    public static AMono TryFetchInScene<AMono>(string idToFind) where AMono : ViewObjMono
    {
      foreach (var monoToCheck in Object.FindObjectsOfType<AMono>())
        if (monoToCheck.GetType().CheckForInterface<IGenerateID>())
          try
          {

            if (monoToCheck is IGenerateID valueToCheck
                && valueToCheck.viewID.Valid()
                && valueToCheck.viewID.Equals(idToFind))
              return monoToCheck;

          }
          catch (Exception e)
          {
            Console.WriteLine(e);
            throw;
          }
      return null;
    }

    public static AMono TryFetchInScene<AMono>(this IGenerateID idToFind) where AMono : ViewObjMono => TryFetchInScene<AMono>(idToFind.viewID);

    public static ViewCloudMono TryFetchInScene(this CloudShell shell)
    {

      return Object.FindObjectsOfType<ViewCloudMono>().FirstOrDefault(o => o.viewID != null
                                                                           && shell.objId != null
                                                                           && o.viewID.Equals(shell.objId));
    }

    public static void CheckAndAdd<TObj>(this List<IViewContent> values, List<IViewContent> items) where TObj : IViewContent
    {
      if (items.Valid())
        values.AddRange(items);
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

    public static List<GameObject> GatherKids(this Transform obj)
    {
      var items = new List<GameObject>();
      foreach (Transform child in obj)
      {
        items.Add(child.gameObject);
        if (child.childCount > 0)
          items.AddRange(child.GatherKids());
      }
      return items;
    }

    public static void SetMeshVisibilityRecursive(this GameObject obj, bool status)
    {
      var mr = obj.GetComponent<MeshRenderer>();
      if (mr != null)
        mr.enabled = status;

      foreach (Transform child in obj.transform) child.gameObject.SetMeshVisibilityRecursive(status);
    }

    public static void SetLayerRecursively(this GameObject obj, int layer)
    {
      obj.layer = layer;

      foreach (Transform child in obj.transform) child.gameObject.SetLayerRecursively(layer);
    }
  }
}