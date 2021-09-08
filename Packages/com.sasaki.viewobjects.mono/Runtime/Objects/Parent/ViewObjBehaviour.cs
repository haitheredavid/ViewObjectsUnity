using System;
using UnityEngine;
using ViewTo.Objects;
using ViewTo.Objects.Mono.Args;
using ViewTo.Objects.Structure;

namespace ViewTo.Connector.Unity
{

  public abstract class ViewObjBehaviour : MonoBehaviour, IValidator
  {
    public abstract ViewObj GetViewObj { get; }
    public abstract bool isValid { get; }
    public abstract void TryImport(ViewObj obj);

    protected abstract void ImportValidObj();
    protected void TriggerImportArgs(ViewObjArgs args) => OnViewObjectImported?.Invoke(args);

    public event Action<ViewObjArgs> OnViewObjectImported;
  }

  public abstract class ViewObjBehaviour<TObj> : ViewObjBehaviour where TObj : ViewObj, new()
  {

    private TObj _internalObj;

    public override ViewObj GetViewObj
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