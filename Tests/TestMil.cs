using System.Collections.Generic;
using UnityEngine;
using ViewTo;
using ViewTo.AnalysisObject;
using ViewTo.StudyObject;
using ViewTo.ViewObject;

namespace ViewToUnity.Tests
{
  public static class TestMil
  {

    public static RigParameters RigParams
    {
      get =>
        new RigParameters
        {
          bundles = ViewerBundle()
        };
    }

    public static Mesh GetPrimitiveMesh(PrimitiveType t)
    {
      var mf = GameObject.CreatePrimitive(t).GetComponent<MeshFilter>();

      Mesh mesh;
      if (Application.isPlaying)
      {
        mesh = Object.Instantiate(mf.mesh);
        Object.Destroy(mf.gameObject);
      }
      else
      {
        mesh = Object.Instantiate(mf.sharedMesh);
        Object.DestroyImmediate(mf.gameObject);
      }


      return mesh;
    }
    public static double RV
    {
      get => Random.Range(0, 100);
    }
    public static ViewStudy Study
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
          layouts = new List<IViewerLayout>
          {
            new ViewerLayout(), new ViewerLayoutCube()
          }
        };

        var bundle2 = new ViewerBundleLinked
        {
          layouts = new List<IViewerLayout>
          {
            new ViewerLayoutCube(), new ViewerLayoutFocus()
          },

          linkedClouds = new List<CloudShell>
          {
            Shell(cloud2)
          }
        };

        var target1 = new TargetContent
        {
          viewName = "GlobalFunSpot",
          objects = new List<object>()
          {
            GetPrimitiveMesh(PrimitiveType.Sphere), GetPrimitiveMesh(PrimitiveType.Cube)
          }
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
          blockers = new List<BlockerContent>
          {
            new BlockerContent(), new BlockerContent()
          },
          designs = new List<DesignContent>
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


        s.objs = new List<object>
        {
          cloud1, cloud2, content, bundle1, bundle2
        };

        return s;
      }
    }

    public static RigParametersIsolated RigParamsIso(ViewCloud c) => new RigParametersIsolated
    {
      bundles = ViewerBundle(c), colors = new List<ViewColor>
        { new ViewColor(255, 255, 255, 255, 0) }
    };

    public static TargetContent TC(bool isolate) => new TargetContent
    {
      viewName = "TestName",
      isolate = isolate,
      bundles = isolate ? ViewerBundle(Cloud(100)) : ViewerBundle()
    };

    public static List<double> ResultValues(int count)
    {
      var values = new List<double>();
      for (int i = 0; i < count; i++)
        values.Add(Random.Range(0, 1));

      return values;
    }

    public static DesignContent DC() => new DesignContent { viewName = "TestName" };

    public static ViewCloud Cloud(int count)
    {
      var pts = new CloudPoint[count];
      for (var i = 0; i < pts.Length; i++)
        pts[i] = new CloudPoint(RV, RV, RV) { meta = "Floor1" };

      return new ViewCloud
        { points = pts };
    }

    public static CloudShell Shell(ViewCloud c) => new CloudShell(c, c.viewID, c.points.Length);

    public static List<ViewerBundle> ViewerBundle(ViewCloud c) => new List<ViewerBundle>
    {
      new ViewerBundleLinked
      {
        linkedClouds = new List<CloudShell>
        {
          Shell(c)
        },
        layouts = new List<IViewerLayout>
        {
          new ViewerLayoutFocus()
        }
      }
    };
    public static List<ViewerBundle> ViewerBundle() => new List<ViewerBundle>
    {
      new ViewerBundle
      {
        layouts = new List<IViewerLayout>
        {
          new ViewerLayout(), new ViewerLayoutCube()
        }
      }
    };
  }

}