using Game.Characters;
using Tools;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Bombs
{
  internal class GameBombView : MonoBehaviour
  {
    public struct Ctx
    {
      public ReactiveProperty<(int indexCharacter, float damage)> receiveDamage;
      public ReactiveProperty<int> destroyBomb;
      public int index;
      public float damage;
      public CompositeDisposable viewDisposable;
    }

    private Ctx _ctx;

    public void SetCtx(Ctx ctx)
    {
      _ctx = ctx;
      gameObject.OnTriggerEnter2DAsObservable().Subscribe(collision =>
      {
        if (collision.gameObject.CompareTag(Tags.TAG_CHARACTER))
        {
          GameCharacterView gameCharacterView = collision.gameObject.GetComponent<GameCharacterView>();
          if (gameCharacterView != null)
            _ctx.receiveDamage.Value = (gameCharacterView.Index, _ctx.damage);
        }
        _ctx.destroyBomb.Value = _ctx.index;
      }).AddTo(_ctx.viewDisposable);
      gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * 1.8f;
    }
  }
}

