using System.Collections.Generic;
using Tools;

namespace Data
{
  internal class GameData : BaseDisposable, IGameData
  {
    public struct Ctx
    {
      public List<Character> characters;
      public List<Bomb> bombs;
      public int countSpawnBomb;
      public int countSpawnCharacter;
    }

    private readonly Ctx _ctx;

    public GameData(Ctx ctx)
    {
      _ctx = ctx;
    }

    public IReadOnlyList<Bomb> Bombs => _ctx.bombs;
    public IReadOnlyList<Character> Characters => _ctx.characters;
    public int CountSpawnBomb => _ctx.countSpawnBomb;
    public int CountSpawnCharacter => _ctx.countSpawnCharacter;
  }
}

