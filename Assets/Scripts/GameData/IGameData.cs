using System.Collections.Generic;

namespace Data
{
  internal interface IGameData
  {
    IReadOnlyList<Bomb> Bombs { get; }
    IReadOnlyList<Character> Characters { get; }
    int CountSpawnBomb { get; }
    int CountSpawnCharacter { get; }
  } 
}

