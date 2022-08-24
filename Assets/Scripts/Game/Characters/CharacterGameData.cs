using System;
using UniRx;

namespace Game.Characters
{
  internal struct CharacterGameData
  {
    public ReactiveProperty<float> CurrentHp;
    public IDisposable CharacterLogic;
    public IDisposable UpdateHp;
  }
}

