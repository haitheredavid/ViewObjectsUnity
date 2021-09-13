using System;
using UnityEngine;
using ViewTo.Connector.Unity;
using ViewToUnity.Tests;

namespace ViewTo.Objects.Mono
{
  [ExecuteAlways]
  public class Test : MonoBehaviour
  {

    [SerializeField] private bool run;
    [SerializeField] private ViewStudyMono study;

    private void Update()
    {
      if(!run) return;
      
      if(study != null)
        MonoHelper.SafeDestroy(study.gameObject);

      study = TestMil.Study.ToViewMono();
      Debug.Log($"Study setup worked? {study!= null}");
      run = false;
    }

  }
}