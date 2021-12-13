using UnityEngine;
using ViewTo.Objects.Mono.Extensions;

namespace ViewTo.Connector.Unity
{
  public class ContentObj : MonoBehaviour
  {
    [SerializeField] private Material mat;

    public Color32 MatColor
    {
      set => mat.color = value;
    }

    public void SetParent(ViewContentMono parent, Material dataAnalysisMaterial)
    {
      mat = dataAnalysisMaterial;

      var meshRend = gameObject.GetComponent<MeshRenderer>();
      if (meshRend == null)
        meshRend = gameObject.AddComponent<MeshRenderer>();

      mat.color = parent.ViewColor.ToUnity();
      if (Application.isPlaying)
        meshRend.material = mat;
      else
        meshRend.sharedMaterial = mat;

    }
  }
}