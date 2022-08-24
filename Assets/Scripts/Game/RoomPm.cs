using Tools;
using UniRx;
using UnityEngine;

namespace Game
{
    internal class RoomPm : BaseDisposable
    {
        public struct Ctx
        {
            public IResourceLoader resourceLoader;
            public ReactiveProperty<RoomLoadedData> loadedRoom;
        }

        private readonly Ctx _ctx;
        private const string PATH_TO_PREFAB = "Prefab/RoomView";

        public RoomPm(Ctx ctx)
        {
            _ctx = ctx;
            GameObject prefab = _ctx.resourceLoader.LoadPrefab(PATH_TO_PREFAB);
            GameObject roomObj = Object.Instantiate(prefab, null, false);
            RoomView roomView = roomObj.GetComponent<RoomView>();
            AddGameObject(roomObj);
            _ctx.loadedRoom.Value = new RoomLoadedData
            {
                placeForElement = roomView.PlaceForElements,
                positionForBomb = roomView.PositionForBomb,
                positionForCharacter = roomView.PositionForCharacter
            };
        }
    }
}

