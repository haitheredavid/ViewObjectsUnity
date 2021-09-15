using System.Collections.Generic;
using UnityEngine;
using ViewTo.AnalysisObject;

namespace ViewTo.Connector.Unity
{
  public class SoRig : ScriptableObject
  {

    public List<ViewCloudMono> globalCloud;

    public List<ViewColor> globalColors;

    public List<SoRigParam> rigParams;
    
    public void Init(Rig obj)
    {
      name = obj.TypeName();

      globalColors = obj.globalColors;

      globalCloud = new List<ViewCloudMono>();
      foreach (var cloudShell in obj.clouds)
      {
        var sceneObj = MonoHelper.TryFetchInScene<ViewCloudMono>(cloudShell.Key);

        if (sceneObj != null)
          globalCloud.Add(sceneObj);
      }
      
      rigParams = new List<SoRigParam>();
      foreach (var g in obj.globalParams)
      {
        var soParam = g.SoCreate(globalColors);
        
        if (soParam.isolate)
          rigParams.Add(soParam);
        else
          rigParams.Insert(0, soParam);
      }
      
    }
  }
}