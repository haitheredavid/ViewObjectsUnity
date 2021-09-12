using System.Collections.Generic;
using ViewTo.StudyObject;

namespace ViewTo.Connector.Unity
{
  public class ResultCloudMono : CloudBehaviour<ResultCloud>, IResultCloud<PixelData>
  {
    public List<PixelData> data { get; set; }
  }
}