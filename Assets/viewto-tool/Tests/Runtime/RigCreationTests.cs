using NUnit.Framework;
using UnityEngine;
using ViewTo;
using ViewTo.Connector.Unity;
using ViewTo.Objects;

namespace ViewToUnity.Tests.Units
{
  public class RigCreationTests
  {
    [TestCase(true)]
    [TestCase(false)]
    public void To_RigObj(bool isValid)
    {
      var s = isValid ? TestMil.Study : new ViewStudy();
      var o = new RigObj();

      s.LoadStudyToRig(ref o);
      Assert.IsTrue(o.CanRun() == isValid);

      var mono = o.ToUnity();
      Assert.True(mono.hasViewObj == isValid);

      if (!isValid) return;

      var res = mono.TestCompile();
      Assert.NotNull(res);
      Debug.Log(res.message);
    }
  }

}