using System.Collections;
using System.Collections.Generic;
using Tools;
using UniRx;
using UnityEngine;

namespace Game.Characters
{
  internal class GameCharacterPm : BaseDisposable
  {
    public struct Ctx
    {
      public int index;
      public string pathToPrefab;
      public IResourceLoader resourceLoader;
      public Transform parent;
      public Vector3 spawnPosition;
    }

    private readonly Ctx _ctx;

    public GameCharacterPm(Ctx ctx)
    {
      _ctx = ctx;
      GameObject prefab = _ctx.resourceLoader.LoadPrefab(_ctx.pathToPrefab);
      GameObject objCharacter = Object.Instantiate(prefab, _ctx.parent, false);
      AddGameObject(objCharacter);
      GameCharacterView characterView = objCharacter.GetComponent<GameCharacterView>();
      characterView.SetCtx(new GameCharacterView.Ctx
      {
        index = _ctx.index
      });
      float randomX = Random.Range(-1.5f, 1.5f);
      characterView.transform.position = new Vector3(randomX, _ctx.spawnPosition.y);
    }
  }
}

