using FlushGameCase.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlushGameCase.Game
{
    public class UIGem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI amountText;

        public void Initialize(GemInfo gemInfo)
        {
            iconImage.sprite = gemInfo.Icon;
            nameText.text = $"Gem Type: {gemInfo.Name}";
            amountText.text = $"Gem Amount: {DataManager.Instance.UserData.GetGemAmount(gemInfo.Id)}";
        }
    }
}
