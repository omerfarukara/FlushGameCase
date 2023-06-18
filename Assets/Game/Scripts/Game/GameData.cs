using DG.Tweening;
using UnityEngine;

namespace FlushGameCase.Game
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
    public class GameData : ScriptableObject
    {
        [Header("Player Movement")] 
        [SerializeField] private float moveSpeed;

        [Header("Selling Settings")] 
        [SerializeField] private float collectRate = 10f;
        
        [Header("Collector Settings")] 
        [SerializeField] private float stackUpCoefficient = 0.5f;
        [SerializeField] private float collectTime;
        [SerializeField] private Ease collectEase;
        
        [Header("Gem Settings")] 
        [SerializeField] private float growthTime = 5f;
        [SerializeField] private Ease growthEase;

        public float MoveSpeed => moveSpeed;
        public float CollectRate => collectRate;
        public float StackUpCoefficient => stackUpCoefficient;
        public float CollectTime => collectTime;
        public Ease CollectEase => collectEase;
        public float GrowthTime => growthTime;
        public Ease GrowthEase => growthEase;
    }
}
