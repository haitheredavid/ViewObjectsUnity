using System;
using UnityEngine;
using ViewTo.Objects.Mono.Extensions;

namespace ViewTo.Connector.Unity
{
  [ExecuteAlways]
  public class ViewCloudMono : ViewObjMono, IViewCloud
  {

    [SerializeField] private string id;

    [SerializeField] private CloudPoint[] cloudPoints;

    private void Awake()
    {
      id ??= Guid.NewGuid().ToString();
    }

    public string viewID
    {
      get => id;
    }

    public CloudPoint[] points
    {
      get => cloudPoints;
      set => cloudPoints = value;
    }

    public int count
    {
      get => this.GetCount();
    }
  }

}