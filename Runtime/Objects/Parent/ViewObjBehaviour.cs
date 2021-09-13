using System;
using UnityEngine;
using ViewTo.Objects.Mono.Args;

namespace ViewTo.Connector.Unity
{

  public abstract class ViewObjBehaviour : MonoBehaviour, IValidator
  {
    public abstract void TryImport(ViewObj obj);

    public abstract bool isValid { get; }
    
    protected abstract void ImportValidObj();
    
    protected void TriggerImportArgs(ViewObjArgs args) => OnViewObjectImported?.Invoke(args);

    public event Action<ViewObjArgs> OnViewObjectImported;
  }

  public abstract class ViewObjBehaviour<TObj> : ViewObjBehaviour where TObj : ViewObj, new()
  {

    private TObj _internalObj;

    public virtual ViewObj GetViewObj
    {
      get => viewObj;
    }
    public TObj viewObj
    {
      get => _internalObj ??= new TObj();
      protected set => _internalObj = value;
    }

    public override bool isValid
    {
      get => !(viewObj is IValidator va) || va.isValid;
    }

    public override void TryImport(ViewObj obj)
    {
      if (obj is TObj casted)
      {
        viewObj = casted;
        ImportValidObj();
      }

    }
  }

}