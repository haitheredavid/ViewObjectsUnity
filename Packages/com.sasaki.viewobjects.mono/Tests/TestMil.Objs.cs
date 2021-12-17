using System;
using System.Collections.Generic;
using UnityEngine;
using ViewTo.ViewObject;

namespace ViewTo.Objects.Mono.Tests
{
  public static partial class TestMil
  {
    public static TObj Init<TObj>() where TObj : ViewObjMono => new GameObject().AddComponent<TObj>();

    private static T Instance<T>()
    {
      var t = (T)Activator.CreateInstance(typeof(T));
      return t;
    }

    public static IViewContentBundle CreateContentBundle()
    {
      var mono = Init<ContentBundleMono>();

      mono.contents = new List<IViewContent>
      {
        CreateContent("Target1"),
        CreateContent("Target2", CreateViewerBundles(CreateViewerLayouts<ViewerLayoutHorizontal>()), true),
        CreateContent<ContentBlockerMono>("Blocker1"),
        CreateContent<ContentBlockerMono>("Blocker2"),
        CreateContent<ContentDesignMono>("Design1"),
        CreateContent<ContentDesignMono>("Design2")
      };

      return mono;
    }

    public static ITargetContent CreateContent(string name)
    {
      var obj = Init<ContentTargetMono>();
      obj.viewName = name;
      return obj;
    }

    public static ITargetContent CreateContent(string name, List<IViewerBundle> bundles, bool isolate)
    {
      var obj = Init<ContentTargetMono>();
      obj.viewName = name;
      obj.bundles = bundles;
      obj.isolate = isolate;
      return obj;
    }

    public static IViewContent CreateContent<TContent>(string name) where TContent : ViewContentMono
    {
      var obj = Init<TContent>();
      obj.viewName = name;
      return(IViewContent)obj;
    }

    public static IViewCloud CreateCloud(int count)
    {
      var obj = Init<ViewCloudMono>();
      obj.points = CreatePoints(count);
      return obj;
    }

    public static IResultCloud CreateCloud(int pointCount, int resultCount)
    {
      var obj = Init<ResultCloudMono>();
      obj.points = CreatePoints(pointCount);
      obj.data = CreateData(resultCount);
      return obj;
    }

    public static List<IResultData> CreateData(int count)
    {
      var data = new List<IResultData>();
      for (var i = 0; i < count; i++)
        data.Add(new ContentResultData(ResultValues(count), "Target", $"Item{i}", default));

      return data;
    }

    public static CloudPoint[] CreatePoints(int count)
    {
      var pts = new CloudPoint[count];
      for (var i = 0; i < count; i++)
        pts[i] = new CloudPoint(Rando, Rando, Rando) { meta = "Floor1" };

      return pts;
    }

    public static IViewerBundle CreateViewerBundle(List<IViewerLayout> layouts)
    {
      var obj = Init<ViewerBundleMono>();
      obj.layouts = layouts;
      return obj;
    }

    public static List<IViewerBundle> CreateViewerBundles(List<IViewerLayout> layouts) => new List<IViewerBundle>
      { CreateViewerBundle(layouts) };

    public static IViewerBundleLinked CreateViewerBundle(List<CloudShell> clouds, List<IViewerLayout> layouts)
    {
      var obj = Init<ViewerBundleLinkedMono>();
      obj.layouts = layouts;
      obj.linkedClouds = clouds;
      return obj;
    }

    public static List<IViewerLayout> CreateViewerLayouts<TLayout>() where TLayout : ViewerLayout => new List<IViewerLayout>
      { CreateViewerLayout<TLayout>() };

    public static IViewerLayout CreateViewerLayout<TLayout>() where TLayout : ViewerLayout
    {
      var obj = Init<ViewerLayoutMono>();
      obj.SetData(Instance<TLayout>());
      return obj;
    }
  }
}