using System.Collections.Generic;
using UnityEngine;

namespace ViewTo.Objects.Mono
{
  public class SoResultData : ScriptableObject
  {

    public List<ContentResultData> data;

    public void Init(List<IResultData> viewObjData)
    {
      if (!viewObjData.Valid())
        return;

      data = new List<ContentResultData>();
      foreach (var res in viewObjData)
        data.Add(new ContentResultData(res.values, res.stage, res.content, res.color, res.meta, res.layout));
    }
  }
}