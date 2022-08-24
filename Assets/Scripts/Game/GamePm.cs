using Data;
using Game.Bombs;
using Game.Characters;
using Tools;
using UniRx;

namespace Game
{
  internal class GamePm : BaseDisposable
  {
    public struct Ctx
    {
      public IResourceLoader resourceLoader;
      public IGameData gameData;
    }

    private readonly Ctx _ctx;

    public GamePm(Ctx ctx)
    {
      _ctx = ctx;
      GameManagerPm.Ctx gameManagerCtx = new GameManagerPm.Ctx
      {
        gameData = _ctx.gameData
      };
      GameManagerPm gameManagerPm = new GameManagerPm(gameManagerCtx);
      AddSubscription(gameManagerPm);
      ReactiveProperty<RoomLoadedData> loadedRoom = new ReactiveProperty<RoomLoadedData>();
      RoomPm.Ctx roomCtx = new RoomPm.Ctx
      {
        resourceLoader = _ctx.resourceLoader,
        loadedRoom = loadedRoom
      };
      RoomPm roomPm = new RoomPm(roomCtx);
      AddSubscription(roomPm);
      AddSubscription(loadedRoom.Subscribe(roomSpawnData =>
      {
        if(roomSpawnData.placeForElement == null)
          return;
        ReactiveProperty<(int indexCharacter, float damage)> receiveDamage =
          new ReactiveProperty<(int indexCharacter, float damage)>();
        BombSpawnerPm.Ctx bombSpawnerCtx = new BombSpawnerPm.Ctx
        {
          receiveDamage = receiveDamage,
          resourceLoader = _ctx.resourceLoader,
          parentForBombs = roomSpawnData.placeForElement,
          countBombForSpawn = _ctx.gameData.CountSpawnBomb,
          initializeBombs = _ctx.gameData.Bombs,
          positionForBomb = roomSpawnData.positionForBomb
        };
        BombSpawnerPm bombSpawnerPm = new BombSpawnerPm(bombSpawnerCtx);
        AddSubscription(bombSpawnerPm);
        CharactersBehaviourPm.Ctx characterBehaviourCtx = new CharactersBehaviourPm.Ctx
        {
          countCharacterForSpawn = _ctx.gameData.CountSpawnCharacter,
          initializeCharacters = _ctx.gameData.Characters,
          parentForCharacters = roomSpawnData.placeForElement,
          receiveDamage = receiveDamage,
          resourceLoader = _ctx.resourceLoader,
          positionForCharacters = roomSpawnData.positionForCharacter
        };
        CharactersBehaviourPm charactersBehaviourPm = new CharactersBehaviourPm(characterBehaviourCtx);
        AddSubscription(charactersBehaviourPm);
      }));
    }
  }
}

