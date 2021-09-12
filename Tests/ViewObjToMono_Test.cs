using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ViewTo;
using ViewTo.AnalysisObject;
using ViewTo.Connector.Unity;
using ViewTo.StudyObject;
using ViewTo.ViewObject;

namespace ViewToUnity.Tests.Units
{

  [TestFixture]
  public class ViewObjToMono_Test
  {

    [TestCase(true)]
    [TestCase(false)]
    public void To_ViewCloud(bool isValid)
    {
      var pts = new CloudPoint[100];
      for (var i = 0; i < pts.Length; i++)
        pts[i] = new CloudPoint(TestMil.RV, TestMil.RV, TestMil.RV) { meta = "Floor1" };

      var o = new ViewCloud
        { points = isValid ? pts : null };

      var mono = o.ToViewMono();
      Assert.NotNull(mono);
      Assert.True(mono.isValid == isValid);

      if (isValid)
        Assert.True(mono.Points.Length == pts.Length == isValid);

    }

    [TestCase(true)]
    [TestCase(false)]
    public void To_Study(bool isValid)
    {
      var o = isValid ? TestMil.Study : new ViewStudy();

      var mono = o.ToViewMono();
      Assert.NotNull(mono);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void To_ViewerBundle(bool isValid)
    {

      var o = isValid ? new ViewerBundle
      {
        layouts = new List<IViewerLayout>
        {
          new ViewerLayout(), new ViewerLayoutCube()
        }
      } : new ViewerBundle();

      var mono = o.ToViewMono();
      Assert.NotNull(mono);

      if (isValid) Assert.False(mono.hasLinks);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void To_ViewerLinkedBundle(bool isValid)
    {

      var o = isValid ? new ViewerBundleLinked
      {
        linkedClouds = new List<CloudShell>
        {
          TestMil.Shell(TestMil.Cloud(100))
        },
        layouts = new List<IViewerLayout>
        {
          new ViewerLayout(), new ViewerLayoutCube()
        }
      } : new ViewerBundle();

      var mono = o.ToViewMono();
      Assert.NotNull(mono);

      if (isValid) Assert.True(mono.hasLinks);

    }

    [TestCase(true)]
    [TestCase(false)]
    public void To_RigObj(bool isValid)
    {
      var shared = TestMil.Cloud(1000);
      var iso = TestMil.Cloud(200);
      var o = isValid ? new Rig
      {
        globalParams = new List<IRigParam>
        {
          TestMil.RigParams
        },
        globalColors = new List<ViewColor>
        {
          new ViewColor(100, 100, 100, 100, 0), new ViewColor(0, 0, 0, 0, 0)
        },
        clouds = new Dictionary<string, CloudPoint[]>
        {
          { shared.viewID, shared.points }, { iso.viewID, iso.points }
        }
      } : new Rig();

      var mono = o.ToViewMono();
      Assert.NotNull(mono);


    }

    [TestCase(true)]
    [TestCase(false)]
    public void To_ContentBundle(bool isValid)
    {
      var o = isValid ? new ContentBundle
      {
        targets = new List<TargetContent>
        {
          TestMil.TC(false),
          TestMil.TC(true)
        },
        blockers = new List<BlockerContent>
        {
          new BlockerContent(), new BlockerContent()
        },
        designs = new List<DesignContent>
        {
          TestMil.DC(),
          TestMil.DC()
        }
      } : new ContentBundle();

      var mono = o.ToViewMono();
      Assert.NotNull(mono);

      if (isValid)
        Assert.True(mono.GetAll.Count() == o.targets.Count + o.blockers.Count + o.designs.Count);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void To_TargetContent(bool isValid)
    {

      var global = isValid ? new TargetContent
      {
        viewName = "TestName",
        bundles = TestMil.ViewerBundle()
      } : new TargetContent();

      var iso = isValid ? new TargetContent
      {
        viewName = "TestName",
        bundles = TestMil.ViewerBundle(TestMil.Cloud(100)),
        isolate = true
      } : new TargetContent();


      var mono = global.ToViewMono();
      Assert.NotNull(mono);

      if (isValid)
        Assert.True(mono.bundles.Count == global.bundles.Count);


      mono = iso.ToViewMono();
      Assert.NotNull(mono);


      if (isValid)
        Assert.True(mono.bundles.Any(bundle => bundle is ViewerBundleLinked));

    }

    [Test]
    public void To_BlockerContent()
    {
      var o = new BlockerContent();
      var mono = o.ToViewMono();

      Assert.NotNull(mono);

    }

    [TestCase(true)]
    [TestCase(false)]
    public void To_DesignContent(bool isValid)
    {
      var o = new DesignContent
        { viewName = isValid ? "TestName" : null };

      var mono = o.ToViewMono();
      Assert.NotNull(mono);


    }
  }

}