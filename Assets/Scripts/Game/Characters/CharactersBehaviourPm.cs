using System.Collections.Generic;
using Data;
using Tools;
using UniRx;
using UnityEngine;

namespace Game.Characters
{
  internal class CharactersBehaviourPm : BaseDisposable
  {
    public struct Ctx
    {
      public IReadOnlyList<Character> initializeCharacters;
      public int countCharacterForSpawn;
      public IReadOnlyReactiveProperty<(int indexCharacter, float damage)> receiveDamage;
      public Transform parentForCharacters;
      public IResourceLoader resourceLoader;
      public Vector3 positionForCharacters;
    }

    private readonly Ctx _ctx;
    private readonly ReactiveDictionary<int, CharacterGameData> _currentCharacters;

    public CharactersBehaviourPm(Ctx ctx)
    {
      _ctx = ctx;
      _currentCharacters = new ReactiveDictionary<int, CharacterGameData>();
      for (int i = 0; i < _ctx.countCharacterForSpawn; i++)
      {
        Character character = _ctx.initializeCharacters[Random.Range(0, _ctx.initializeCharacters.Count)];
        CreateCharacter(i,character);
      }
      AddSubscription(_ctx.receiveDamage.Subscribe(damageData =>
      {
        if (_currentCharacters.TryGetValue(damageData.indexCharacter, out CharacterGameData characterGameData))
          characterGameData.CurrentHp.Value -= damageData.damage;
      }));
    }

    private void CreateCharacter(int index, Character character)
    {
      ReactiveProperty<float> currentHp = new ReactiveProperty<float>(character.Hp);
      GameCharacterPm.Ctx gameCharacterCtx = new GameCharacterPm.Ctx
      {
        index = index,
        pathToPrefab = character.PathToPrefab,
        resourceLoader = _ctx.resourceLoader,
        parent = _ctx.parentForCharacters,
        spawnPosition = _ctx.positionForCharacters
      };
      GameCharacterPm gameCharacterPm = new GameCharacterPm(gameCharacterCtx);
      AddSubscription(gameCharacterPm);
      CharacterGameData characterGameData = new CharacterGameData
      {
        CharacterLogic = gameCharacterPm,
        CurrentHp = currentHp,
        UpdateHp = currentHp.Subscribe(hp =>
        {
          if (hp <= 0)
            RemoveCharacter(index);
        })
      };
      _currentCharacters.Add(index, characterGameData);
    }

    private void RemoveCharacter(int index)
    {
      if (_currentCharacters.TryGetValue(index, out CharacterGameData characterGameData))
      {
        characterGameData.UpdateHp?.Dispose();
        characterGameData.CharacterLogic?.Dispose();
        _currentCharacters.Remove(index);
      }
    }

    protected override void OnDispose()
    {
      if (_currentCharacters != null)
      {
        foreach (KeyValuePair<int, CharacterGameData> characterGameDataPair in _currentCharacters)
        {
          characterGameDataPair.Value.CharacterLogic?.Dispose();
          characterGameDataPair.Value.UpdateHp?.Dispose();
        }
        _currentCharacters.Clear();
      }
      base.OnDispose();
    }
  }
}

