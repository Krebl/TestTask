using UnityEngine;

namespace Game.Characters
{
  internal class GameCharacterView : MonoBehaviour
  {
    public struct Ctx
    {
      public int index;
    }

    private Ctx _ctx;

    public int Index => _ctx.index;

    public void SetCtx(Ctx ctx)
    {
      _ctx = ctx;
    }
  }
}

