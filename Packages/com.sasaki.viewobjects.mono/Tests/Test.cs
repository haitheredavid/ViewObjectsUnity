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

    [SerializeField] private bool create;
    [SerializeField] private bool update;
    [SerializeField] private bool prime;
    [SerializeField] private ViewStudyMono study;

    private void Update()
    {
      if (update && study != null)
      {
        update = false;
        foreach (var o in study.objs)
          if (o is ContentBundleMono mono)
            mono.ChangeColors();
      }

      if (prime && study != null)
      {
        prime = false;
        foreach (var o in study.objs)
          if (o is ContentBundleMono mono)
            mono.Prime();
      }

      if (!create) return;

      if (study != null)
        MonoHelper.SafeDestroy(study.gameObject);

      study = TestMil.Study.ToViewMono();
      Debug.Log($"Study setup worked? {study != null}");

      create = false;
    }

  }
}