using System;
using System.Collections.Generic;
using FlushGameCase.Core;
using FlushGameCase.Game.Interfaces;
using UnityEngine;

namespace FlushGameCase.Game.Player
{
    public class Seller : MonoBehaviour
    {
        private readonly Stack<CollectedGem> _gems = new Stack<CollectedGem>();

        private float _nextTimeToCollect;
        private float _collectRate;

        private void OnEnable()
        {
            _collectRate = DataManager.Instance.GameData.CollectRate;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_gems.Count == 0) return;

            if (other.TryGetComponent(out SellArea sellArea))
            {
                if (Time.time >= _nextTimeToCollect)
                {
                    SellGem(sellArea);
                    _nextTimeToCollect = Time.time + 1f / _collectRate;
                }
            }
        }

        public void AddGem(CollectedGem collectedGem)
        {
            _gems.Push(collectedGem);
        }

        private void SellGem(SellArea sellArea)
        {
            CollectedGem sellingGem = _gems.Pop();
            Transform sellingGemTransform = sellingGem.transform;
            
            float scale = sellingGemTransform.localScale.x;
            
            sellArea.Sell(sellingGem.GemInfo, sellingGemTransform.position, scale);
            DataManager.Instance.UserData.IncreaseGold(sellingGem.GemInfo.Price, scale);
            sellingGem.CompleteTask();
        }
    }
}
