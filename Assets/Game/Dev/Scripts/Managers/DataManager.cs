using FlushGameCase.Game;
using FlushGameCase.Game.Player;
using UnityEngine;

namespace FlushGameCase.Core
{
    public class DataManager : MonoSingleton<DataManager>
    {
        [SerializeField] private EventData eventData;
        [SerializeField] private GemData gemData;
        [SerializeField] private GameData gameData;
        [SerializeField] private UserData userData;
        public EventData EventData => eventData;
        public GemData GemData => gemData;
        public GameData GameData => gameData;
        public UserData UserData => userData;
    }
}
