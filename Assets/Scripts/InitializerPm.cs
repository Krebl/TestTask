using System.Collections.Generic;
using Data;
using Game;
using Tools;

internal class InitializerPm : BaseDisposable
{
  public struct Ctx
  {
    
  }

  private readonly Ctx _ctx;

  public InitializerPm(Ctx ctx)
  {
    _ctx = ctx;
    ResourceLoaderTools resourceLoaderTools = new ResourceLoaderTools();
    GameData.Ctx gameDataCtx = new GameData.Ctx
    {
      characters = new List<Character>
      {
        new Character
        {
          Hp = 100,
          PathToPrefab = "Prefab/character"
        },
        new Character
        {
          Hp = 300,
          PathToPrefab = "Prefab/character"
        }
      },
      bombs = new List<Bomb>
      {
        new Bomb
        {
          Damage = 40,
          PathToPrefab = "Prefab/bomb"
        },
        new Bomb
        {
          Damage = 56,
          PathToPrefab = "Prefab/bomb"
        }
      },
      countSpawnBomb = 100,
      countSpawnCharacter = 3
    };
    GameData gameData = new GameData(gameDataCtx);
    AddSubscription(gameData);
    GamePm.Ctx gameCtx = new GamePm.Ctx
    {
      gameData = gameData,
      resourceLoader = resourceLoaderTools
    };
    GamePm gamePm = new GamePm(gameCtx);
    AddSubscription(gamePm);
  }
}
