using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using ViewTo.Connector.Unity;
using ViewTo.Objects;
using ViewTo.Structure;

[TestFixture]
public class ViewObjToMono_Test
{

  [TestCase(true)]
  [TestCase(false)]
  public void To_ViewCloud(bool isValid)
  {
    var pts = new CloudPoint[100];
    for (int i = 0; i < pts.Length; i++)
      pts[i] = new CloudPoint(RV, RV, RV) {meta = "Floor1"};

    var o = new ViewCloud
      {points = isValid ? pts : null};

    var mono = o.ToUnity();
    Assert.True(mono.hasViewObj == isValid);

    if (isValid)
      Assert.True(mono.Points.Length == pts.Length == isValid);

  }

  [TestCase(true)]
  [TestCase(false)]
  public void To_Study(bool isValid)
  {
    var o = isValid ? Study : new ViewStudy();

    var mono = o.ToUnity();
    Assert.True(mono.hasViewObj == isValid);
  }
  
  [TestCase(true)]
  [TestCase(false)]
  public void To_ViewerBundle(bool isValid)
  {

    var o = isValid ? new ViewerBundle
    {
      layouts = new List<ViewerLayout>
      {
        new ViewerLayout(), new ViewerLayoutCube()
      }
    } : new ViewerBundle();

    var mono = o.ToUnity();
    Assert.True(mono.hasViewObj == isValid);

    if (isValid)
    {
      Assert.False(mono.hasLinks);
      Assert.True(mono.viewerCount == o.layouts.Sum(l => l.viewers.Count));
    }

  }
  [TestCase(true)]
  [TestCase(false)]
  public void To_ViewerLinkedBundle(bool isValid)
  {

    var o = isValid ? new ViewerBundleLinked()
    {
      linkedClouds = new List<MetaShell>
        {Shell(Cloud(100))},
      layouts = new List<ViewerLayout>
      {
        new ViewerLayout(), new ViewerLayoutCube()
      }
    } : new ViewerBundle();

    var mono = o.ToUnity();
    Assert.True(mono.hasViewObj == isValid);

    if (isValid)
    {
      Assert.True(mono.hasLinks);
      Assert.True(mono.viewerCount == o.layouts.Sum(l => l.viewers.Count));
    }

  }

  [TestCase(true)]
  [TestCase(false)]
  public void To_ContentBundle(bool isValid)
  {
    var o = isValid ? new ContentBundle()
    {
      targets = new List<TargetContent>()
      {
        TC(false), TC(true)
      },
      blockers = new List<BlockerContent>()
      {
        new BlockerContent(), new BlockerContent()
      },
      designs = new List<DesignContent>
        {DC(), DC()}
    } : new ContentBundle();

    var mono = o.ToUnity();
    Assert.True(mono.hasViewObj == isValid);
    if (isValid)
      Assert.True(mono.Contents.Count() == o.targets.Count + o.blockers.Count + o.designs.Count);
  }

  #region view content
  [TestCase(true)]
  [TestCase(false)]
  public void To_TargetContent(bool isValid)
  {

    var global = isValid ? new TargetContent
    {
      viewName = "TestName",
      bundles = ViewerBundle()
    } : new TargetContent();

    var iso = isValid ? new TargetContent
    {
      viewName = "TestName",
      bundles = ViewerBundle(Cloud(100)),
      isolate = true
    } : new TargetContent();


    var mono = global.ToUnity();
    Assert.True(mono.hasViewObj == isValid);
    if (isValid)
      Assert.True(mono.bundles.Count == global.bundles.Count);


    mono = iso.ToUnity();
    Assert.True(mono.hasViewObj == isValid);

    if (isValid)
      Assert.True(mono.bundles.Any(bundle => bundle is ViewerBundleLinked));

  }

  [Test]
  public void To_BlockerContent()
  {
    var o = new BlockerContent();
    var mono = o.ToUnity();

    Assert.NotNull(mono);

  }

  [TestCase(true)]
  [TestCase(false)]
  public void To_DesignContent(bool isValid)
  {
    var o = new DesignContent
      {viewName = isValid ? "TestName" : null};

    var mono = o.ToUnity();
    Assert.True(mono.hasViewObj == isValid);

  }
  #endregion

  #region helpers
  private double RV => Random.Range(0, 100);

  private TargetContent TC(bool isolate)
  {
    return new TargetContent
    {
      viewName = "TestName",
      isolate = isolate,
      bundles = isolate ? ViewerBundle(Cloud(100)) : ViewerBundle(),
    };
  }

  private DesignContent DC()
  {
    return new DesignContent {viewName = "TestName"};
  }

  private ViewCloud Cloud(int count)
  {
    var pts = new CloudPoint[count];
    for (int i = 0; i < pts.Length; i++)
      pts[i] = new CloudPoint(RV, RV, RV) {meta = "Floor1"};

    return new ViewCloud
      {points = pts};
  }

  private MetaShell Shell(ViewCloud c)
  {
    return new MetaShell(c, c.viewID, c.points.Length);
  }

  private List<ViewerBundle> ViewerBundle(ViewCloud c)
  {
    return new List<ViewerBundle>
    {
      new ViewerBundleLinked
      {
        linkedClouds = new List<MetaShell>
        {
          Shell(c)
        },
        layouts = new List<ViewerLayout>
        {
          new ViewerLayoutFocus()
        }
      }
    };

  }

  private ViewStudy Study
  {
    get
    {
      var s = new ViewStudy
      {
        viewName = "StudyCloudTest"
      };

      var cloud1 = Cloud(1000);
      var cloud2 = Cloud(100);

      var bundle1 = new ViewerBundle
      {
        layouts = new List<ViewerLayout>
        {
          new ViewerLayout(), new ViewerLayoutCube()
        }
      };

      var bundle2 = new ViewerBundleLinked
      {
        layouts = new List<ViewerLayout>
        {
          new ViewerLayoutCube(), new ViewerLayoutFocus()
        },

        linkedClouds = new List<MetaShell>
        {
          Shell(cloud2)
        }
      };

      var target1 = new TargetContent
      {
        viewName = "GlobalFunSpot"
      };

      var target2 = new TargetContent
      {
        viewName = "IsolatedTarget",
        isolate = true,
        bundles = new List<ViewerBundle>
        {
          bundle2
        }
      };


      var content = new ContentBundle
      {
        targets = new List<TargetContent>
        {
          target1, target2
        },
        blockers = new List<BlockerContent>()
        {
          new BlockerContent(), new BlockerContent()
        },
        designs = new List<DesignContent>()
        {
          new DesignContent
          {
            viewName = "Design1"
          },
          new DesignContent
          {
            viewName = "Design2"
          }
        }
      };

      var rigParams = new RigParameters
      {
        bundles = new List<ViewerBundle>
        {
          bundle1, bundle2
        }
      };

      s.objs = new List<ViewObj>
      {
        cloud1, cloud2, content, rigParams
      };

      return s;
    }
  }

  private List<ViewerBundle> ViewerBundle()
  {
    return new List<ViewerBundle>
    {
      new ViewerBundle
      {
        layouts = new List<ViewerLayout>
        {
          new ViewerLayout(), new ViewerLayoutCube()
        }
      }
    };
  }
  #endregion

}