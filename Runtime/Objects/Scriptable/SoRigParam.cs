using System.Collections.Generic;
using UnityEngine;

namespace ViewTo.Connector.Unity
{
  public class SoRigParam : ScriptableObject
  {
    public bool isolate;
    public List<ViewColor> contentColors;
    public List<ViewCloudMono> linkedClouds;
    public List<SoViewerBundle> viewers;
  }
}