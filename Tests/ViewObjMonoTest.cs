using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ViewObjects;
using ViewObjects.Viewer;
using ViewTo.Objects;
using ViewTo.Objects.Mono;

[TestFixture]
  public class ViewObjMonoTest
  {

    [Test]
    public void To_ViewCloud()
    {
      var mono = TestMil.CreateCloud(100);
      Assert.NotNull(mono);
      Assert.True(mono.points.Valid() && mono.points.Length == 100);
    }

    [Test]
    public void To_ResultCloud()
    {
      var mono = TestMil.CreateCloud(100, 6);
      Assert.NotNull(mono);
      Assert.True(mono.points.Valid() && mono.points.Length == 100);
      Assert.True(mono.data.Valid() && mono.data.Count == 6);
    }

    [Test]
    public void To_Study()
    {
      var mono = TestMil.Init<ViewStudyMono>();
      mono.viewName = "ViewStudy";

      mono.objs = new List<IViewObj>
      {
        TestMil.CreateContentBundle(),
        TestMil.CreateCloud(100),
        TestMil.CreateViewerBundle(TestMil.CreateViewerLayouts<ViewerLayoutHorizontal>())
      };

      Assert.NotNull(mono);
      Assert.True(mono.gameObject.name.Equals(mono.viewName));
      Assert.True(mono.objs.Valid());
      Assert.True(mono.isValid);
    }

    [Test]
    public void To_ViewerBundle()
    {
      var mono = TestMil.CreateViewerBundle(TestMil.CreateViewerLayouts<ViewerLayoutHorizontal>());

      Assert.NotNull(mono);
      Assert.True(mono.layouts.Valid());
    }

    [Test]
    public void To_ViewerLinkedBundle()
    {

      var clouds = new List<IViewCloud> { TestMil.CreateCloud(100), TestMil.CreateCloud(25) }
        .Select(x => x.GetShell()).ToList();

      var mono = TestMil.CreateViewerBundle(clouds, TestMil.CreateViewerLayouts<ViewerLayoutHorizontal>());

      Assert.NotNull(mono);
      Assert.True(mono.layouts.Valid());
      Assert.True(mono.linkedClouds.Valid());
    }

    [Test]
    public void To_ContentBundle()
    {
      var mono = TestMil.CreateContentBundle();

      Assert.NotNull(mono);
      Assert.True(mono.contents.Valid());
    }

    [Test]
    public void To_TargetContent()
    {
      var mono = TestMil.CreateContent("globalTarget", TestMil.CreateViewerBundles(TestMil.CreateViewerLayouts<ViewerLayoutHorizontal>()), false);

      Assert.NotNull(mono);
      Assert.True(mono.viewName.Valid());
      Assert.True(mono.bundles.Valid());
      Assert.False(mono.objects.Valid());

      Assert.False(mono.isolate);
    }

    [Test]
    public void To_TargetContentIsolate()
    {
      var mono = TestMil.CreateContent("isolateTarget", TestMil.CreateViewerBundles(TestMil.CreateViewerLayouts<ViewerLayoutHorizontal>()), true);
      Assert.NotNull(mono);
      Assert.True(mono.viewName.Valid());
      Assert.True(mono.bundles.Valid());
      Assert.False(mono.objects.Valid());

      Assert.True(mono.isolate);
    }

    [Test]
    public void To_BlockerContent()
    {
      var mono = TestMil.CreateContent<ContentBlockerMono>("test");
      Assert.NotNull(mono);
      Assert.True(mono.viewName.Valid());
    }

    [Test]
    public void To_DesignContent()
    {
      var mono = TestMil.CreateContent<ContentDesignMono>("test");
      Assert.NotNull(mono);
      Assert.True(mono.viewName.Valid());

    }
  }
  