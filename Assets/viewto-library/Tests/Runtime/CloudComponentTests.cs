using NUnit.Framework;
using ViewTo;
using ViewTo.Connector.Unity;

namespace Integration
{


  public class CloudComponentTests
  {

    [Test, Description( "Simple logic testing for setting a view cloud with points" )]
    public void Setup_AddCloudPointsToRenderer( )
      {
        var obj = ComponentMil.Build<ViewCloudMono>( Mil.Fabricate.Object.ViewCloud(  ) );
        Assert.IsNotEmpty( obj.Points );

      }

  }
}