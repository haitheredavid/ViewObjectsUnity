using System.Collections.Generic;

namespace ViewTo.Connector.Unity
{

  public class TargetContentMono : ViewContentMono, ITargetContent
  {
    public bool isolate { get; set; }
    public List<IViewerBundle> bundles { get; set; }
  }
}