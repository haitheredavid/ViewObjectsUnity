using UnityEngine;
using ViewTo.Objects;
using ViewTo.Objects.Structure;

namespace ViewTo.Connector.Unity
{

  public abstract class ViewObjBehaviour : MonoBehaviour, IValidator
  {
    public abstract void TryImport(ViewObj obj);
    public abstract bool isValid { get; }
    public abstract ViewObj GetViewObj { get; }

    protected abstract void ImportValidObj();
  }

  public abstract class ViewObjBehaviour<TObj> : ViewObjBehaviour where TObj : ViewObj, new()
  {

    public override ViewObj GetViewObj => viewObj;

    private TObj _internalObj;
    public TObj viewObj
    {
      get => _internalObj ??= new TObj();
      protected set => _internalObj = value;
    }

    public override bool isValid
    {
      get { return!(viewObj is IValidator va) || va.isValid; }
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