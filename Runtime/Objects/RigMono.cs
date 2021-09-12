using System.Collections.Generic;
using System.Linq;
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
  public class RigMono : ViewObjBehaviour<Rig>
  {

    [SerializeField] [Range(0, 300)] private int frameRate = 180;
    [SerializeField] private bool isRunning;

    /// <summary>
    /// the points data that is handed to the rig after build
    /// </summary>
    public List<CloudShellUnity> globalPoints
    {
      get { return viewObj.clouds != null && viewObj.clouds.Any() ? viewObj.clouds.ToUnity() : null; }
      set => viewObj.clouds = value.ToView();
    }

    /// <summary>
    /// global colors used for analysis stage.
    /// colors are set from core command where view study builds out rig
    /// </summary>
    public List<ViewColor> globalColors
    {
      get => viewObj.globalColors;
      set => viewObj.globalColors = value;
    }

    /// <summary>
    /// types of viewers to be using
    /// </summary>
    public List<IRigParam> @params
    {
      get => viewObj.globalParams;
      set => viewObj.globalParams = value;
    }

    protected override void ImportValidObj()
    {
      name = viewObj.TypeName();
    }
  }
}