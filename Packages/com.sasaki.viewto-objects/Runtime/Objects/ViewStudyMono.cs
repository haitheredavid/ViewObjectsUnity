using System;
using UnityEngine;
using ViewTo.Objects;

namespace ViewTo.Connector.Unity
{
  public class ViewStudyMono : ViewObjBehaviour<ViewStudy>
  {
    
    [SerializeField] private string viewName;

    protected override void ImportValidObj()
    {
      viewName = viewObj.viewName;
    }
    

  }
}