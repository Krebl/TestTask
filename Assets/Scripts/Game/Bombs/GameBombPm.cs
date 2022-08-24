using Data;
using Tools;
using UniRx;
using UnityEngine;

namespace Game.Bombs
{
  internal class GameBombPm : BaseDisposable
  {
    public struct Ctx
    {
      public ReactiveProperty<(int indexCharacter, float damage)> receiveDamage;
      public ReactiveProperty<int> destroyBomb;
      public int index;
      public Bomb bomb;
      public IResourceLoader resourceLoader;
      public Transform parent;
      public Vector3 spawnPosition;
    }

    private readonly Ctx _ctx;

    public GameBombPm(Ctx ctx)
    {
      _ctx = ctx;
      GameObject prefab = _ctx.resourceLoader.LoadPrefab(_ctx.bomb.PathToPrefab);
      GameObject objBomb = Object.Instantiate(prefab, _ctx.parent, false);
      AddGameObject(objBomb);
      GameBombView bombView = objBomb.GetComponent<GameBombView>();
      CompositeDisposable viewDisposable = new CompositeDisposable();
      AddSubscription(viewDisposable);
      bombView.SetCtx(new GameBombView.Ctx
      {
        receiveDamage = _ctx.receiveDamage,
        destroyBomb = _ctx.destroyBomb,
        index = _ctx.index,
        damage = _ctx.bomb.Damage,
        viewDisposable = viewDisposable
      });
      float randomX = Random.Range(-2.5f, 2.5f);
      bombView.transform.position = new Vector3(randomX, _ctx.spawnPosition.y);
    }
  }
}

