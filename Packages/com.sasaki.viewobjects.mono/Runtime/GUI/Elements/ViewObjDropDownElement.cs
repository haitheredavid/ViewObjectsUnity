using System;
using ViewTo;
using ViewTo.Objects.Mono.Args;

public abstract class ViewObjDropDownElement<TObj> : SimpleDropForObjects<TObj> where TObj : ViewObj
{
  public event EventHandler<ViewObjArgs<TObj>> onViewObjUpdate;


  protected abstract ViewObjArgs<TObj> CreateArgs(TObj obj);

  protected void ViewObjUpdated(TObj obj)
  {
    onViewObjUpdate?.Invoke(this, CreateArgs(obj));
  }

  protected override string FormatName(TObj obj) => obj.TypeName();

  public override void SetValue(TObj obj)
  {
    base.SetValue(obj);

    var viewObj = ProcessObjParams(obj);
    if (viewObj != null)
      ViewObjUpdated(viewObj);
  }
}