using FlushGameCase.Core;
using UnityEngine;

namespace FlushGameCase.Game.Player
{
    [CreateAssetMenu(fileName = "UserData", menuName = "Data/UserData")]
    public class UserData : ScriptableObject
    {
        public int Gold
        {
            get => PlayerPrefs.GetInt("Gold");
            set => PlayerPrefs.SetInt("Gold", value);
        }
        
        public void IncreaseGemAmount(int id)
        {
            PlayerPrefs.SetInt($"Gem{id}",PlayerPrefs.GetInt($"Gem{id}") + 1);
        }

        public int GetGemAmount(int id)
        {
            return PlayerPrefs.GetInt($"Gem{id}");
        }

        public void IncreaseGold(int price, float scale)
        {
            int earningGold = price + (int)(100 * scale);
            Gold += earningGold;
            DataManager.Instance.EventData.OnUpdateGold?.Invoke(Gold);
        }
    }
}
