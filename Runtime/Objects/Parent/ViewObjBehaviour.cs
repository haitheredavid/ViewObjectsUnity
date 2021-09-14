using System;
using UnityEngine;
using ViewTo.Objects.Mono.Args;

namespace ViewTo.Connector.Unity
{

  public abstract class ViewObjBehaviour : MonoBehaviour
  {
    public abstract void TryImport(ViewObj obj);

    protected void TriggerImportArgs(ViewObjArgs args) => OnViewObjectImported?.Invoke(args);

    public event Action<ViewObjArgs> OnViewObjectImported;
  }

  public abstract class ViewObjBehaviour<TObj> : ViewObjBehaviour where TObj : ViewObj, new()
  {

    protected abstract void ImportValidObj(TObj viewObj);

    public override void TryImport(ViewObj obj)
    {
      if (obj is TObj casted)
        ImportValidObj(casted);

    }
  }

}