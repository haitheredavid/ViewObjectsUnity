namespace ViewTo.Connector.Unity
{
  public interface IToSource<TObj> where TObj : IViewObj
  {
    public TObj GetRef { get; }
    public void SetRef(TObj obj);
  }
}