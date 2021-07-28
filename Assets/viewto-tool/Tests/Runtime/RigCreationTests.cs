using NUnit.Framework;
using ViewTo.Connector.Unity;
using ViewTo.Core.Mil;
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
      Assert.NotNull(mono);
    }
  }

}