using System.Collections.Generic;
using UnityEngine;

namespace ViewToUnity.Tests
{
  public static partial class TestMil
  {

    public static double Rando
    {
      get => Random.Range(0, 100);
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

    public static List<double> ResultValues(int count)
    {
      var values = new List<double>();
      for (var i = 0; i < count; i++)
        values.Add(Random.Range(0, 1));

      return values;
    }
  }
}