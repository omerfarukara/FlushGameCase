using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlushGameCase.Game
{
    [CreateAssetMenu(fileName = "GemData", menuName = "Data/GemData")]
    public class GemData : ScriptableObject
    {
        [SerializeField] private GemInfo[] gemTypes;

        public GemInfo GetRandomGemInfo()
        {
            return gemTypes[Random.Range(0, gemTypes.Length)];
        }

        private void OnValidate()
        {
            for (int i = 0; i < gemTypes.Length; i++)
            {
                gemTypes[i].Id = i;
            }
        }

        public GemInfo[] GetAllGems()
        {
            return gemTypes;
        }
    }
}
