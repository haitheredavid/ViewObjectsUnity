using System.Collections.Generic;
using UnityEngine;
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

    public List<RigParameters> GlobalBundles => viewObj.GlobalBundles;
    public List<RigParametersIsolated> IsolatedBundles => viewObj.IsolatedBundles;
    public List<ViewColor> GlobalColors => viewObj.GlobalColors;

    protected override void ImportValidObj()
    {
      points = new List<CloudShell>();
      foreach (var cloud in viewObj.Points)
        points.Add(new CloudShell
                     {id = cloud.Key, count = cloud.Value.Length, points = cloud.Value.ToUnity()});

    }
  }
}