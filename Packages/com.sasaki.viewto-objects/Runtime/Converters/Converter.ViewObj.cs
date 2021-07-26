using System.Linq;
using UnityEngine;
using ViewTo.Objects;

namespace ViewTo.Connector.Unity
{
  public static partial class ViewConverter
  {

    public static string TypeName(this object obj) => obj.GetType().ToString().Split('.').Last();
    
        
    public static ViewerBundleMono ToUnity(this ViewerBundle obj, bool importIfValid = true)
    {
      return ToUnity<ViewerBundleMono>(obj, importIfValid);
    }
    
    
    public static ViewStudyMono ToUnity(this ViewStudy obj, bool importIfValid = true)
    {
      return ToUnity<ViewStudyMono>(obj, importIfValid);
    }

    
    public static ContentBundleMono ToUnity(this ContentBundle obj, bool importIfValid = true)
    {
      return ToUnity<ContentBundleMono>(obj, importIfValid);
    }

    public static TargetContentMono ToUnity(this TargetContent obj, bool importIfValid = true)
    {
      return ToUnity<TargetContentMono>(obj, importIfValid);
    }

    public static BlockerContentMono ToUnity(this BlockerContent obj, bool importIfValid = true)
    {
      return ToUnity<BlockerContentMono>(obj, importIfValid);
    }

    public static DesignContentMono ToUnity(this DesignContent obj, bool importIfValid = true)
    {
      return ToUnity<DesignContentMono>(obj, importIfValid);
    }

    public static ViewCloudMono ToUnity(this ViewCloud obj, bool importIfValid = true)
    {
      return ToUnity<ViewCloudMono>(obj, importIfValid);
    }

    public static TShell ToUnity<TShell>(this ViewObj obj, bool importIfValid = true) where TShell : ViewObjBehaviour
    {
      var shell = (TShell)new GameObject().AddComponent(typeof(TShell));

      if (importIfValid)
        shell.TryImport(obj);

      return shell;
    }

  }
}