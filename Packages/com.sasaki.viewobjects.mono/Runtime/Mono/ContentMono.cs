using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ViewObjects;
using Object = UnityEngine.Object;

namespace ViewTo.Objects.Mono
{
  public abstract class ContentMono : ViewObjMono
  {
    [SerializeField] private string contentName;

    [SerializeField] [HideInInspector] private int mask;

    [SerializeField] private List<ContentObj> contentObjs;

    [SerializeField] private int colorId;
    [SerializeField] private Color32 color;

    [SerializeField] private ClassTypeReference objType;
    private static int DiffuseColor => Shader.PropertyToID("_diffuseColor");

    public List<object> objects
    {
      get => contentObjs.Valid() ? contentObjs.Cast<object>().ToList() : new List<object>();
      set
      {
        contentObjs = new List<ContentObj>();

        foreach (var o in value)
        {
          if (o is Object obj)
          {
            ContentObj content;

            switch (obj)
            {
              case Component comp:
                comp.transform.SetParent(transform);
                content = comp.GetComponent<ContentObj>();
                if (content == null)
                  content = comp.gameObject.AddComponent<ContentObj>();
                break;
              case GameObject go:
                go.transform.SetParent(transform);
                content = go.GetComponent<ContentObj>();
                if (content == null)
                  content = go.gameObject.AddComponent<ContentObj>();
                break;
              default:
                Debug.LogWarning($"I don't know how to handle {obj.TypeName()}");
                content = null;
                break;
            }

            contentObjs.Add(content);
          }
        }
      }
    }

    public int contentLayerMask
    {
      get => mask;
      set => mask = value;
    }

    public ViewColor viewColor
    {
      get => new ViewColor(color.r, color.g, color.b, color.a);
      set
      {
        if (value == null)
          return;

        Debug.Log($"new assigned to {contentName}:" + value.ToUnity());

        color = value.ToUnity();

        ApplyColor();

      }
    }

    public string viewName
    {
      get => contentName;
      set
      {
        contentName = value;
        gameObject.name = FullName;
      }
    }

    public string FullName
    {
      get { return this.TypeName() + "-" + viewName; }
    }

    private void ApplyColor()
    {
      if (contentObjs.Valid())
        foreach (var contentObj in contentObjs)
        {
          contentObj.SetColor = color;
        }
    }

    /// <summary>
    ///   references the objects converted to the view content list and imports them
    /// </summary>
    public void PrimeMeshData(Material material, Action<ContentObj> onAfterPrime = null)
    {
      if (!objects.Valid())
      {
        Debug.Log($"No objects for {name} are ready to be primed ");
        return;
      }

      if (material == null)
      {
        Debug.LogError($"Material is needed to prime mesh data on {name}");
        return;
      }

      var c = color;
      Debug.Log("color=" + c.ToString());
      Debug.Log("viewcolor=" + viewColor.ToUnity());

      if (material.HasProperty(DiffuseColor))
        material.SetColor(DiffuseColor, c);
      else
        Debug.Log($"No property {DiffuseColor} on shader");


      gameObject.ApplyAll(material);
      gameObject.SetLayerRecursively(contentLayerMask);
      // this little loop is taking care of all the filtering of what speckle might send back. ideally it will be just components
      foreach (var obj in contentObjs) 
        onAfterPrime?.Invoke(obj);

      // TODO: why is this not setting
    }
  }
}