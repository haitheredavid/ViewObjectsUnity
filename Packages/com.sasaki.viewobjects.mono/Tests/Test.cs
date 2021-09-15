using System;
using UnityEngine;
using ViewTo.AnalysisObject;
using ViewTo.Connector.Unity;
using ViewToUnity.Tests;
using Object = UnityEngine.Object;

namespace ViewTo.Objects.Mono
{
  [ExecuteAlways]
  public class Test : MonoBehaviour
  {

    [SerializeField] private bool run;
    [SerializeField] private bool build;
    [SerializeField] private ViewStudyMono study;

    private void Update()
    {
      if (build && study != null)
      {
        foreach (var objBehaviour in study.objs)
        {
          if (objBehaviour is ViewerBundleMono mono) 
            mono.CreateViewers();
        }
        build = false;
      }

      if (!run) return;

      if (study != null)
        MonoHelper.SafeDestroy(study.gameObject);

      study = TestMil.Study.ToViewMono();
      Debug.Log($"Study setup worked? {study != null}");


      run = false;


    }

  }
}