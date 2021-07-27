using System.Collections.Generic;
using System.Linq;
using HaiThere.Utilities;
using UnityEngine;
using ViewTo.Events.Reports;
using ViewTo.Objects;
using ViewTo.Structure;

namespace ViewTo.Connector.Unity
{
  public class RigMono : ViewObjBehaviour<RigObj>
  {
    [Header("|| Params ||")]
    [ReadOnly] [SerializeField] private List<CloudShell> points;
    [ReadOnly] [SerializeField] private int totalBundleCount;

    [Header("|| Runtime||")]
    [SerializeField] [Range(0, 300)] private int frameRate = 180;
    [SerializeField] private bool isRunning;

    public List<RigParameters> globalBundles;
    public List<RigParametersIsolated> isolatedBundles;
    public List<ViewColor> globalColors;

    protected override void ImportValidObj()
    {
      isolatedBundles = viewObj.isolatedParams.Valid() ? viewObj.isolatedParams : new List<RigParametersIsolated>();
      globalBundles = viewObj.globalParams.Valid() ? viewObj.globalParams : new List<RigParameters>();
      globalColors = viewObj.globalColors.Valid() ? viewObj.globalColors : new List<ViewColor>();
      points = viewObj.clouds != null && viewObj.clouds.Any() ? viewObj.clouds.ToUnity() : new List<CloudShell>();
    }

    public RigSetupReportArgs TestCompile()
    {
      var bundles = new List<ViewerBundle>();
      foreach (var gb in globalBundles.Where(gb => gb.bundles.Valid())) bundles.AddRange(gb.bundles);
      return new RigSetupReportArgs(bundles, globalColors, points.Valid() ? points.ToView() : new List<CloudInfo>());
    }
  }

}