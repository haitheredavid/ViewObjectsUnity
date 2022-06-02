using System.Collections.Generic;
using ViewObjects;

namespace ViewTo.Objects.Mono
{

  public class ContentTargetMono : ContentMono, ITargetContent
  {
    public bool isolate { get; set; }
    public List<IViewerBundle> bundles { get; set; }
  }
}