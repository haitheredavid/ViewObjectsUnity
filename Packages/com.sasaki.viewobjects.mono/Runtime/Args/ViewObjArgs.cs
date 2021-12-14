using System;

namespace ViewTo.Objects.Mono.Args
{
  public abstract class ViewObjArgs : EventArgs
  { }

  public abstract class ViewObjArgs<TObj> : ViewObjArgs where TObj : IViewObj
  {
    public readonly TObj viewObj;
    public ViewObjArgs(TObj @object) => viewObj = @object;
  }

}