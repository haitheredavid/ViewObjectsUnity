using UnityEngine;
using ViewTo.Objects;
using ViewTo.Structure;

namespace ViewTo.Connector.Unity
{

  public abstract class ViewContentMono : ViewObjBehaviour<ViewContent>
  {

    [SerializeField] protected Color32 viewColor = Color.magenta;
    [ReadOnly] [SerializeField] protected int viewColorID;
    [ReadOnly] [SerializeField] protected int maskMask;

    public int ContentMask
    {
      get => maskMask;
    }

    public ViewColor ViewColor
    {
      get => viewColor.ToNative(viewColorID);
      protected set
      {
        viewColor = value.ToUnity();
        viewColorID = value.Id;
      }
    }
  }

  public abstract class ViewContentMono<TContent> : ViewContentMono
    where TContent : ViewContent
  {

    protected override void ImportValidObj()
    {
      if (viewObj is TContent casted)
        ImportValidObj(casted);
    }

    protected virtual void ImportValidObj(TContent content)
    {
      maskMask = MaskByType(content);
      ViewColor = viewObj.viewColor;
    }

    private static int MaskByType(ViewContent t)
    {
      return t switch
      {
        DesignContent _ => 6,
        TargetContent _ => 7,
        BlockerContent _ => 8,
        _ => 0
      };
    }

  }

}