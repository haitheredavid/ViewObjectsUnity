using UnityEngine;
using ViewTo.ViewObject;

namespace ViewTo.Connector.Unity
{

  public abstract class ViewByTypeContentMono : ViewObjBehaviour<ViewContent>
  {
    [SerializeField] private Color32 viewColor = Color.magenta;
    [SerializeField] private int viewColorID;
    [SerializeField] private int contentMask;

    public int ContentMask
    {
      get => contentMask;
      set => contentMask = value;
    }

    public ViewColor ViewColor
    {
      get => viewObj.viewColor;
      set
      {
        viewObj.viewColor = value;
        viewColor = value.ToUnity();
        viewColorID = value.Id;
      }
    }

    protected override void ImportValidObj()
    {
      ContentMask = MaskByType();
      SetMeshData();
      SetContentData(viewObj);
    }

    /// <summary>
    ///   references the objects converted to the view content list and imports them
    /// </summary>
    protected virtual void SetMeshData()
    {
      if (!viewObj.objects.Valid()) return;

      foreach (var obj in viewObj.objects)
        if (obj is GameObject go)
        {
          go.transform.SetParent(transform);
        }
        else if (obj is Mesh mesh)
        {
          var mf = new GameObject().AddComponent<MeshFilter>();
          mf.gameObject.AddComponent<MeshRenderer>();

          if (Application.isPlaying)
            mf.mesh = mesh;
          else
            mf.sharedMesh = mesh;

          mf.transform.SetParent(transform);
        }

    }

    protected abstract void SetContentData(ViewContent t);

    private int MaskByType() => viewObj switch
    {
      DesignContent _ => 6,
      TargetContent _ => 7,
      BlockerContent _ => 8,
      _ => 0
    };
  }

  public abstract class ViewByTypeContentMono<TContent> : ViewByTypeContentMono
    where TContent : ViewContent, new()
  {

    protected virtual void SetValidContent(TContent content)
    {
      gameObject.name = content.TypeName();
    }

    protected override void SetContentData(ViewContent t)
    {
      if (t is TContent casted)
        SetValidContent(casted);
    }
  }

}