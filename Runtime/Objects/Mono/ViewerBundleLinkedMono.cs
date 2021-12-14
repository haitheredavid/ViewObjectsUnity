using System.Collections.Generic;

namespace ViewTo.Connector.Unity
{
  public class ViewerBundleLinkedMono : ViewObjMono, IViewerBundleLinked
  {
    public List<IViewerLayout> layouts { get; set; }
    public List<CloudShell> linkedClouds { get; set; }
  }
}