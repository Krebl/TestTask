using System;
using System.Collections.Generic;
using Data;
using Tools;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Bombs
{
  internal class BombSpawnerPm : BaseDisposable
  {
    public struct Ctx
    {
      public ReactiveProperty<(int indexCharacter, float damage)> receiveDamage;
      public IReadOnlyList<Bomb> initializeBombs;
      public int countBombForSpawn;
      public IResourceLoader resourceLoader;
      public Transform parentForBombs;
      public Vector3 positionForBomb;
    }

    private readonly Ctx _ctx;
    private const float SECONDS_DELAY_SPAWN = 0.5f;
    private int _countSpawnedBombs;
    private Dictionary<int, IDisposable> _currentBombs;
    private ReactiveProperty<int> _destroyBomb;

    public BombSpawnerPm(Ctx ctx)
    {
      _ctx = ctx;
      _currentBombs = new Dictionary<int, IDisposable>();
      _destroyBomb = new ReactiveProperty<int>();
      AddSubscription(Observable.Interval(TimeSpan.FromSeconds(SECONDS_DELAY_SPAWN))
        .Where(_ => _countSpawnedBombs < _ctx.countBombForSpawn).Subscribe(_ =>
        {
          _countSpawnedBombs++;
          Bomb bomb = _ctx.initializeBombs[Random.Range(0, _ctx.initializeBombs.Count)];
          CreateBomb(_countSpawnedBombs, bomb);
        }));
      AddSubscription(_destroyBomb.Skip(1).Subscribe(index =>
      {
        if (_currentBombs.TryGetValue(index, out IDisposable logicBomb))
        {
          logicBomb?.Dispose();
          _currentBombs.Remove(index);
        }
      }));
    }

    private void CreateBomb(int index, Bomb bomb)
    {
      GameBombPm.Ctx gameBombCtx = new GameBombPm.Ctx
      {
        receiveDamage = _ctx.receiveDamage,
        destroyBomb = _destroyBomb,
        index = index,
        bomb = bomb,
        resourceLoader = _ctx.resourceLoader,
        parent = _ctx.parentForBombs,
        spawnPosition = _ctx.positionForBomb
      };
      GameBombPm gameBombPm = new GameBombPm(gameBombCtx);
      _currentBombs.Add(index, gameBombPm);
    }

    protected override void OnDispose()
    {
      if (_currentBombs != null)
      {
        foreach (KeyValuePair<int, IDisposable> bombData in _currentBombs)
        {
          bombData.Value?.Dispose();
        }
        _currentBombs.Clear();
      }
      base.OnDispose();
    }
  }
}

