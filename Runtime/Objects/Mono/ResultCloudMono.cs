using System.Collections.Generic;
using UnityEngine;
using ViewTo.StudyObject;

namespace ViewTo.Connector.Unity
{
  public class ResultCloudMono : CloudMono<ResultCloud>
  {
    [SerializeField] private SoResultData data;

    protected override void ImportValidObj(ResultCloud viewObj)
    {
      base.ImportValidObj(viewObj);

      data = ScriptableObject.CreateInstance<SoResultData>();
      data.Init(viewObj.data);
    }

  }
}