using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using ViewTo;
using ViewTo.Connector.Unity;
using ViewTo.StudyObject;
using ViewTo.ViewObject;

public static class VisualElementHelper
{
  public static TextField CreateNameElement(this ViewContent obj, Action<string> onValueChange = null)
  {
    var element = new TextField("Content Name") { value = obj?.viewName ?? string.Empty };

    element.RegisterValueChangedCallback(evt => onValueChange?.Invoke(evt.newValue));

    return element;
  }

  public static Toggle CreateToggleElement(this TargetContent obj, Action<bool> onValueChange)
  {
    var element = new Toggle("Isolate") { value = obj != null && obj.isolate };

    element.RegisterValueChangedCallback(evt => onValueChange?.Invoke(evt.newValue));

    return element;
  }

  // public static ListView CreateContentListElement(this ViewContentMono mono)
  // {
  //   var container = new ListView();
  //
  //   if (mono == null || !mono.GetSceneObjs.Valid())
  //     return container;
  //
  //
  //   foreach (var o in mono.GetSceneObjs)
  //   {
  //     container.Add(new ObjectField(o.name)
  //     {
  //       name = "content-obj-field",
  //       label = o.name
  //     });
  //   }
  //
  //   return container;
  // }

  public static IntegerField CreateObjectCountElement(this ViewContent obj)
  {
    return new IntegerField
    {
      isReadOnly = true,
      label = "Object Count",
      value = obj.objects.Valid() ? obj.objects.Count : 0
    };

  }

  public static Foldout CreateViewerBundleElement(this ViewerBundle obj)
  {
    var container = new Foldout
    {
      text = obj.TypeName()
    };

    foreach (var l in obj.layouts)
    {
      var vLayoutElement = new ViewerLayoutDropDownElement();
      vLayoutElement.SetValue(l as ViewerLayout);
      container.Add(vLayoutElement);
    }
    return container;
  }
}