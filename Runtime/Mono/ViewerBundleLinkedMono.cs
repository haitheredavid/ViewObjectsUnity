using System.Collections.Generic;
using ViewObjects;
using ViewObjects.Cloud;

namespace ViewTo.Objects.Mono
{
  public class ViewerBundleLinkedMono : ViewObjMono, IViewerBundleLinked
  {
    public List<IViewerLayout> layouts { get; set; }
    public List<CloudShell> linkedClouds { get; set; }
  }
}