using System.Collections.Generic;

namespace ViewTo.Objects.Mono
{

  public class ContentTargetMono : ViewContentMono, ITargetContent
  {
    public bool isolate { get; set; }
    public List<IViewerBundle> bundles { get; set; }
  }
}