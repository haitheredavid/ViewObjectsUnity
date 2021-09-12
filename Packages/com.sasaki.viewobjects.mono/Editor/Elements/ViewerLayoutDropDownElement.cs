using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using ViewTo;
using ViewTo.Connector.Unity;
using ViewTo.Objects.Mono.Args;
using ViewTo.ViewObject;

public class ViewerLayoutDropDownElement : ViewObjDropDownElement<ViewerLayout>
{
  public new class UxmlTraits : VisualElement.UxmlTraits
  { }

  public new class UxmlFactory : UxmlFactory<ViewerLayoutDropDownElement, UxmlTraits>
  { }

  protected override ViewObjArgs<ViewerLayout> CreateArgs(ViewerLayout obj) => new ViewerLayoutArgs(obj);
  protected override ViewerLayout ProcessObjParams(ViewerLayout @object)
  {
    paramRoot.Clear();

    switch (@object)
    {
      case ViewerLayoutFocus o:
        paramRoot.Add(SetLayout(o));
        return o;
      case ViewerLayoutNormal o:
        paramRoot.Add(SetLayout(o));
        return o;
      case ViewerLayoutOrtho o:
        paramRoot.Add(SetLayout(o));
        return o;
      case ViewerLayoutCube o:
        return o;
      case ViewerLayoutHorizontal o:
        return o;
      case { } o:
        return o;
      default:
        return null;

    }
  }

  private VisualElement SetLayout(ViewerLayoutOrtho vl)
  {
    var param = new FloatField
    {
      value = (float)vl.size,
      tooltip = $"{vl.TypeName()} will use this value as camera ortho size"
    };

    param.RegisterValueChangedCallback(evt =>
    {
      ViewObjUpdated(new ViewerLayoutOrtho
      {
        size = evt.newValue
      });
    });
    return param;

  }

  private VisualElement SetLayout(ViewerLayoutNormal vl)
  {
    var param = new ObjectField("Normal Cloud")
    {
      objectType = typeof(ViewCloudMono),
      value = vl.shell.TryFetchInScene(),
      tooltip = $"{vl.TypeName()} requires View Cloud with Normal Data "
    };

    param.RegisterValueChangedCallback(evt =>
    {

      var obj = GameObject.Find(evt.newValue.name);

      CloudShell newShell = new CloudShell();
      if (obj != null)
      {
        var mono = obj.GetComponent<ViewCloudMono>();
        if (mono != null)
          newShell = new CloudShell(mono.viewObj.GetType(), mono.viewID, mono.Points?.Length ?? 0);
      }
      ViewObjUpdated(new ViewerLayoutNormal
      {
        shell = newShell
      });
    });
    return param;
  }

  private VisualElement SetLayout(ViewerLayoutFocus vl)
  {
    var param = new Vector3Field("Focus Point")
    {
      value = new Vector3((float)vl.x, (float)vl.y, (float)vl.z)
    };

    param.RegisterValueChangedCallback(evt =>
    {
      ViewObjUpdated(new ViewerLayoutFocus
      {
        x = evt.newValue.x,
        y = evt.newValue.y,
        z = evt.newValue.z
      });
    });
    return param;
  }
}