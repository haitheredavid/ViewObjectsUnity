using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ViewTo.Objects;
using ViewTo.Objects.Structure;

namespace ViewTo.Connector.Unity
{

  [Serializable]
  public class ViewColorParam
  {
    public Color32 color;
    public int index;
  }

  [Serializable]
  public class RigParam
  {
    public List<ViewerBundle> viewerbundles;
  }

  [Serializable]
  public class RigParamIso : RigParam
  {
    public List<CloudShell> clouds;
    public List<ViewColorParam> colors;
  }

  public class RigMono : ViewObjBehaviour<Rig>
  {

    [Header("|| Runtime||")]
    [SerializeField] [Range(0, 300)] private int frameRate = 180;
    [SerializeField] private bool isRunning;

    [SerializeField] private List<RigParam> @params;

    public List<CloudShell> points
    {
      get => viewObj.clouds != null && viewObj.clouds.Any() ? viewObj.clouds.ToUnity() : null;
      set => viewObj.clouds = value.ToView();
    }

    public List<RigParameters> globalBundles
    {
      get => viewObj.globalParams;
      set => viewObj.globalParams = value;
    }

    public List<ViewColor> globalColors
    {
      get => viewObj.globalColors;
      set => viewObj.globalColors = value;
    }

    public List<RigParametersIsolated> isolatedBundles
    {
      get => viewObj.isolatedParams;
      set => viewObj.isolatedParams = value;
    }

    protected override void ImportValidObj()
    {
      name = viewObj.TypeName();
      @params = new List<RigParam>();


    }

  }
}