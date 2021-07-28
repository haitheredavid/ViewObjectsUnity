using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ViewTo.Connector.Unity
{
  public class RenderViewCam : MonoBehaviour
  {

    // public KeyCode key = KeyCode.A;
    // public List<ViewCamera> ViewCameras = new List<ViewCamera>();
    // public string folder = @"F:\1.Projects\ViewTo\captures\ShaderCam";
    //
    // private Texture2D toTexture2D(RenderTexture rTex)
    // {
    //   Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
    //   RenderTexture.active = rTex;
    //   tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
    //   tex.Apply();
    //   return tex;
    // }
    //
    // public void Update()
    // {
    //   if (Input.GetKeyDown(key))
    //   {
    //     SaveTexture();
    //   }
    //
    // }
    //
    // public void SaveTexture()
    // {
    //
    //   foreach (var viewCamera in ViewCameras)
    //   {
    //     byte[] bytes = toTexture2D(viewCamera.RenderText).EncodeToPNG();
    //     File.WriteAllBytes(Path.Combine(folder, viewCamera.name + ".png"), bytes);
    //   }
    //
    // }

  }

}