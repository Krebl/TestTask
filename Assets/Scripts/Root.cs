using System;
using UnityEngine;

public class Root : MonoBehaviour
{
  private IDisposable _initializer;
  private void Awake()
  {
    InitializerPm.Ctx initializerCtx = new InitializerPm.Ctx();
    _initializer = new InitializerPm(initializerCtx);
  }

  private void OnDestroy()
  {
    _initializer?.Dispose();
  }
}
