using System.Collections.Generic;
using ViewTo.StudyObject;

namespace ViewTo.Connector.Unity
{
  public class ResultCloudMono : CloudMono<ResultCloud>, IResultCloud<PixelData>
  {
    public List<PixelData> data { get; set; }
  }
}