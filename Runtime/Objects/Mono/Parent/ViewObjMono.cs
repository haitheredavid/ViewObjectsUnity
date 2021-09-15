using UnityEngine;

namespace ViewTo.Connector.Unity
{

  public abstract class ViewObjMono : MonoBehaviour
  {
    public abstract void TryImport(ViewObj obj);
  }

  public abstract class ViewObjMono<TObj> : ViewObjMono where TObj : ViewObj, new()
  {

    protected abstract void ImportValidObj(TObj viewObj);

    public override void TryImport(ViewObj obj)
    {
      if (obj is TObj casted)
        ImportValidObj(casted);

    }
  }

}