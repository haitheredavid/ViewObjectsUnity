using UnityEngine.UIElements;
using ViewTo.Objects;
using ViewTo.Objects.Mono.Args;

public class ViewContentDropDownElement : ViewObjDropDownElement<ViewContent>
{

  protected override ViewObjArgs<ViewContent> CreateArgs(ViewContent obj) => new ViewContentArgs(obj);

  //TODO setup structure for view content objects
  protected override ViewContent ProcessObjParams(ViewContent @object)
  {
    paramRoot.Clear();
    switch (@object)
    {
      case TargetContent vc:
        var toggleField = new Toggle("isolate")
        {
          value = vc.isolate
        };
        var nameField = new TextField(vc.viewName);

        paramRoot.Add(nameField);
        paramRoot.Add(toggleField);

        return vc;
      case BlockerContent vc:

        return vc;
      case DesignContent vc:

        return vc;

      default:
        return null;

    }
  }

  public new class UxmlTraits : VisualElement.UxmlTraits
  { }

  public new class UxmlFactory : UxmlFactory<ViewerLayoutDropDownElement, UxmlTraits>
  { }
}