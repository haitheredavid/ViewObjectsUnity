using System.Linq;
using UnityEngine;
using ViewTo.Objects;

namespace ViewTo.Connector.Unity
{
  public static partial class ViewConverter
  {

    public static string TypeName(this object obj) => obj.GetType().ToString().Split('.').Last();

    public static RigMono ToUnity(this RigObj obj, bool importIfValid = true) => obj.ToUnity<RigMono>(importIfValid);

    public static ViewerBundleMono ToUnity(this ViewerBundle obj, bool importIfValid = true) => ToUnity<ViewerBundleMono>(obj, importIfValid);

    public static ViewStudyMono ToUnity(this ViewStudy obj, bool importIfValid = true) => ToUnity<ViewStudyMono>(obj, importIfValid);

    public static ViewCloudMono ToUnity(this ViewCloud obj, bool importIfValid = true) => ToUnity<ViewCloudMono>(obj, importIfValid);

    public static TShell ToUnity<TShell>(this ViewObj obj, bool importIfValid = true) where TShell : ViewObjBehaviour
    {
      var shell = (TShell)new GameObject().AddComponent(typeof(TShell));

      if (importIfValid)
        shell.TryImport(obj);

      return shell;
    }

  }
}