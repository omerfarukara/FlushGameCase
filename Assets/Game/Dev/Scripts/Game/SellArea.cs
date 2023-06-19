using System.Collections.Generic;
using UnityEngine;

namespace FlushGameCase.Game
{
    public class SellArea : MonoBehaviour
    {
        [SerializeField] private CollectedGem collectedGemPrefab;
        [SerializeField] private Transform caseTransform;
        
        private readonly Queue<CollectedGem> _collectedGems = new Queue<CollectedGem>();

        public void Sell(GemInfo gemInfo, Vector3 position, float scale)
        {
            SpawnNewGem(gemInfo, position, scale);
        }
        
        private void SpawnNewGem(GemInfo gemInfo, Vector3 position, float scale)
        {
            if (_collectedGems.Count == 0)
            {
                CollectedGem newCollectedGem = Instantiate(collectedGemPrefab, transform);
                _collectedGems.Enqueue(newCollectedGem);
            }

            CollectedGem collectedGem = _collectedGems.Dequeue();
            Vector3 endPosition = caseTransform.localPosition;
            collectedGem.Collect(gemInfo, scale, position, endPosition, ReturnToQueue, true);
        }

        private void ReturnToQueue(CollectedGem collectedGem)
        {
            _collectedGems.Enqueue(collectedGem);
        }
    }
}
