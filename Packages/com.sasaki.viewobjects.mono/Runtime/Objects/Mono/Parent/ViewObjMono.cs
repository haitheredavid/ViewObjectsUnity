using UnityEngine;

namespace ViewTo.Connector.Unity
{

  
  public abstract class ViewObjMono : MonoBehaviour
  {
    public abstract void TryImport(ViewObj @object);
  }

  public abstract class ViewObjMono<TObj> : ViewObjMono where TObj : ViewObj, new()
  {

    protected abstract void ImportValidObj(TObj viewObj);

    public override void TryImport(ViewObj @object)
    {
      if (@object is TObj casted)
        ImportValidObj(casted);

    }
  }

}