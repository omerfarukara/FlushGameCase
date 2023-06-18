using System;
using System.Collections;
using System.Collections.Generic;
using FlushGameCase.Core;
using FlushGameCase.Game.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FlushGameCase.Game.Player
{
    [RequireComponent(typeof(Seller))]
    public class Collector : MonoBehaviour
    {
        [SerializeField] private CollectedGem collectedGemPrefab;
        [SerializeField] private Transform bagTransform;
        
        private readonly Queue<CollectedGem> _collectedGems = new Queue<CollectedGem>();

        private Seller _seller;

        private int _gemAmount;
        private float _stackUpCoefficient;

        private void Awake()
        {
            _seller = GetComponent<Seller>();
        }

        private void OnEnable()
        {
            _stackUpCoefficient = DataManager.Instance.GameData.StackUpCoefficient;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectable iCollectable))
            {
                if (iCollectable.IsCollectable)
                {
                    iCollectable.CompleteTask();
                    SpawnNewGem(iCollectable.GetGemInfo, iCollectable.GetPosition, iCollectable.GetScale);
                }
            }
        }

        private void SpawnNewGem(GemInfo gemInfo, Vector3 position, float scale)
        {
            if (_collectedGems.Count == 0)
            {
                CollectedGem newCollectedGem = Instantiate(collectedGemPrefab, transform);
                _collectedGems.Enqueue(newCollectedGem);
            }

            _gemAmount++;
            CollectedGem collectedGem = _collectedGems.Dequeue();
            Vector3 endPosition = bagTransform.localPosition + Vector3.up * _gemAmount * _stackUpCoefficient;
            collectedGem.Collect(gemInfo, scale, position, endPosition, ReturnToQueue);
            _seller.AddGem(collectedGem);
            DataManager.Instance.EventData.OnCollectGem?.Invoke(collectedGem.GemInfo.Id);
            DataManager.Instance.UserData.IncreaseGemAmount(collectedGem.GemInfo.Id);
        }

        private void ReturnToQueue(CollectedGem collectedGem)
        {
            _collectedGems.Enqueue(collectedGem);
            _gemAmount--;
        }
    }
}
