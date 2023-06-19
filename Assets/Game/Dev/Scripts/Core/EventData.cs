using System;
using UnityEngine;

namespace FlushGameCase.Core
{
    [CreateAssetMenu(fileName = "EventData", menuName = "Data/EventData")]
    public class EventData : ScriptableObject
    {
        public Action<int> OnCollectGem { get; set; }
        public Action<int, float> OnSellGem { get; set; }
        public Action<int> OnUpdateGold { get; set; }
    }
}
