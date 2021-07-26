using UnityEngine;
using ViewTo.Objects;

namespace ViewTo.Connector.Unity
{
  public class DesignContentMono : ViewContentMono<DesignContent>
  {

    [SerializeField] private string viewName;

    protected override void ImportValidObj(DesignContent content)
    {
      base.ImportValidObj(content);
      viewName = content.viewName;
    }
  }
}