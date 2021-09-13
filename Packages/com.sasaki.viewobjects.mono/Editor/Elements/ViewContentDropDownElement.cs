using System;
using UnityEngine.UIElements;
using ViewTo;
using ViewTo.Objects.Mono.Args;
using ViewTo.ViewObject;

public class ViewContentDropDownElement : ViewObjDropDownElement<ViewContent>
{
  public ViewContentDropDownElement()
  {
    skipBaseObj = true;
  }

  protected override ViewObjArgs<ViewContent> CreateArgs(ViewContent obj) => new ViewContentArgs(obj);

  protected override ViewContent ProcessObjParams(ViewContent @object)
  {
    paramRoot.Clear();
    if (@object is TargetContent vc)
      SetContent(vc);

    return @object;
  }

  public event EventHandler<bool> IsolateUpdated;

  private void SetContent(TargetContent vc)
  {
    paramRoot.Add(vc.CreateToggleElement(fieldValue => IsolateUpdated?.Invoke(this, fieldValue)));

    if (vc.bundles.Valid())
    {
      for (var i = 0; i < vc.bundles.Count; i++)
      {
        var container = new Foldout();
        var b = vc.bundles[i];
        container.text = b.TypeName() + i;

        foreach (var l in b.layouts)
        {
          var vLayoutElement = new ViewerLayoutDropDownElement();

          vLayoutElement.SetValue(l as ViewerLayout);
          // add only to root
          paramRoot.Add(vLayoutElement);
          // set to list
          container.Add(vLayoutElement);

        }
        paramRoot.Add(container);
      }
    }

  }

  public new class UxmlTraits : VisualElement.UxmlTraits
  { }

  public new class UxmlFactory : UxmlFactory<ViewContentDropDownElement, UxmlTraits>
  { }
}