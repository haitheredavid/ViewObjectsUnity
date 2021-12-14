using System;
using System.Linq;
using ViewTo.Objects.Mono;
using Object = UnityEngine.Object;

namespace ViewTo.Objects
{
  public static partial class ViewObjMonoExt
  {

    public static int GetCount(this IViewCloud obj) => obj.points.Valid() ? obj.points.Length : 0;

    public static string FullName(this ViewContentMono obj) => obj.GetRef?.TypeName()[0] + "-" + obj.viewName;
    
    public static AMono TryFetchInScene<AMono>(string idToFind) where AMono : ViewObjMono
    {
      foreach (var monoToCheck in Object.FindObjectsOfType<AMono>())
        if (monoToCheck.GetType().CheckForInterface<IGenerateID>())
          try
          {

            if (monoToCheck is IGenerateID valueToCheck
                && valueToCheck.viewID.Valid()
                && valueToCheck.viewID.Equals(idToFind))
              return monoToCheck;

          }
          catch (Exception e)
          {
            Console.WriteLine(e);
            throw;
          }
      return null;
    }

    public static AMono TryFetchInScene<AMono>(this IGenerateID idToFind) where AMono : ViewObjMono
    {
      return TryFetchInScene<AMono>(idToFind.viewID);
    }

    public static ViewCloudMono TryFetchInScene(this CloudShell shell)
    {

      return Object.FindObjectsOfType<ViewCloudMono>().FirstOrDefault(o => o.viewID != null
                                                                           && shell.objId != null
                                                                           && o.viewID.Equals(shell.objId));
    }

    
    
  }
}