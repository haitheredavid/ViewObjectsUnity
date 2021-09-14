using System.Collections.Generic;
using UnityEngine;
using ViewTo.AnalysisObject;

namespace ViewTo.Connector.Unity
{

  /// <summary>
  /// This object is a bit strange at the moment. The point cloud data it pulls is directly from study object
  /// The cloud point data might need to consider using the view cloud data form the scene if we consider adding point creation in runtime
  ///
  /// should work for now thou. 
  /// </summary>
  [ExecuteAlways]
  public class RigMono : MonoBehaviour
  {

    [SerializeField] [Range(0, 300)] private int frameRate = 180;
    [SerializeField] private bool isRunning;

    /// <summary>
    /// the points data that is handed to the rig after build
    /// </summary>
    [SerializeField] private List<ViewCloudMono> globalPoints;

    /// <summary>
    /// global colors used for analysis stage.
    /// colors are set from core command where view study builds out rig
    /// </summary>
    [SerializeField] private List<ViewColor> globalColors;

    /// <summary>
    /// types of viewers to be using
    /// </summary>
    [SerializeField] private List<SoViewerBundle> globalBundles;

    public void ImportValidObj(Rig viewObj)
    {
      name = viewObj.TypeName();

      globalColors = viewObj.globalColors;
      globalBundles = new List<SoViewerBundle>();

      foreach (var g in viewObj.globalParams)
      {
        foreach (var bundle in g.bundles)
        {
          var b = ScriptableObject.CreateInstance<SoViewerBundle>();
          b.SetRef(bundle);
          globalBundles.Add(b);
        }
      }
    }
  }
}