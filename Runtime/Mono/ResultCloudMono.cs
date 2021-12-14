using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ViewTo.Objects.Mono
{
  public class ResultCloudMono : ViewObjMono, IResultCloud
  {

    [SerializeField] private CloudPoint[] cloudPoints;

    [SerializeField] private string id;

    [SerializeField] [HideInInspector] private List<ContentResultData> cloudData;

    public CloudPoint[] points
    {
      get => cloudPoints;
      set => cloudPoints = value;
    }

    public int count
    {
      get => this.GetCount();
    }

    public string viewID
    {
      get => id;
    }

    public List<IResultData> data
    {
      get => cloudData.Valid() ? cloudData.Cast<IResultData>().ToList() : new List<IResultData>();
      set
      {
        if (!value.Valid())
          return;

        cloudData = new List<ContentResultData>();
        foreach (var item in value)
          AddResultData(item);

      }
    }

    public void AddResultData(IResultData value)
    {
      cloudData ??= new List<ContentResultData>();
      cloudData.Add(new ContentResultData(value.values, value.stage, value.content, value.color, value.meta, value.layout));
    }
  }

}