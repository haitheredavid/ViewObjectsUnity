using HaiThere;
using UnityEngine;
using ViewTo.Objects;

namespace ViewTo.Connector.Unity
{

  public abstract class ViewObjBehaviour : MonoBehaviour
  {
    public abstract void TryImport(ViewObj obj);
  }

  public abstract class ViewObjBehaviour<TObj> : ViewObjBehaviour where TObj : ViewObj
  {

    public TObj viewObj { get; protected set; }
    public bool hasViewObj => viewObj != null;

    public override void TryImport(ViewObj obj)
    {
      switch (obj)
      {
        case null:
          return;
        case TObj casted:
          if (casted is IValidator va && !va.isValid)
            return;

          viewObj = casted;
          ImportValidObj();
          break;
      }
    }

    protected virtual void ImportValidObj() => Debug.Log($"Valid ViewObj {viewObj.TypeName()!} for {this.TypeName()}");
  }

}