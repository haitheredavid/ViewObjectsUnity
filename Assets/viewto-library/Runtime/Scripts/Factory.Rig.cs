using UnityEngine;

namespace ViewTo.Connector.Unity
{
  public static partial class ViewFactory
  {
    public static Rig BuildRig( this ViewStudyMono studyMono )
      {
        if ( studyMono == null || !studyMono.IsReady ) return null;


        return null;
      }
    
  }
}