using System;
using FlushGameCase.Core;
using TMPro;
using UnityEngine;

namespace FlushGameCase.UI
{
    public class UIGold : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI goldText;

        private void OnEnable()
        {
            DataManager.Instance.EventData.OnUpdateGold += UpdateGold;
        }

        private void Start()
        {
            UpdateGold(DataManager.Instance.UserData.Gold);
        }

        private void UpdateGold(int gold)
        {
            goldText.text = $"{gold}";
        }

        private void OnDisable()
        {
            DataManager.Instance.EventData.OnUpdateGold -= UpdateGold;
        }
    }
}
