using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  internal class RoomView : MonoBehaviour
  {
    public struct Ctx
    {
      
    }

    [SerializeField] 
    private Transform _placeForElements;
    [SerializeField] 
    private Transform _transformPositionForCharacter;
    [SerializeField] 
    private Transform _transformPositionForBomb;

    private Ctx _ctx;

    public Transform PlaceForElements 
      => _placeForElements;

    public Vector3 PositionForBomb => 
      _transformPositionForBomb.position;

    public Vector3 PositionForCharacter 
      => _transformPositionForCharacter.position;

    public void SetCtx(Ctx ctx)
    {
      _ctx = ctx;
      
    }
  }
}

