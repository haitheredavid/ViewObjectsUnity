using System.Collections.Generic;

namespace ViewObjects.Unity
{

	public class ContentTargetMono : ContentMono, ITargetContent
	{
		public bool isolate { get; set; }
		public List<IViewerBundle> bundles { get; set; }
	}
}