using Data;
using Tools;

namespace Game
{
  //единственный класс, который может изменять состояние модели данных
  internal class GameManagerPm : BaseDisposable
  {
    public struct Ctx
    {
      public IGameData gameData;
    }

    private readonly Ctx _ctx;

    public GameManagerPm(Ctx ctx)
    {
      _ctx = ctx;
    }
  }
}

