using System;
using UnityEngine;
using ViewObjects;

namespace ViewTo.Objects.Mono
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
      set => id = value;
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