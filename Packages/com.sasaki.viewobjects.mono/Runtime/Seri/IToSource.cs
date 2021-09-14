namespace ViewTo.Connector.Unity
{
  public interface IToSource<TObj> where TObj : ViewObj
  {
    public void SetRef(TObj obj);
    public TObj RefTo { get; }
  }
}