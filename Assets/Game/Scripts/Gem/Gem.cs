using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FlushGameCase.Core;
using FlushGameCase.Game.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace FlushGameCase.Game
{
    public class Gem : SpawnObject, ICollectable
    {
        [Header("General")]
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshRenderer meshRenderer;

        [Header("Events")]
        [SerializeField] private UnityEvent onGrowthComplete;

        private Transform _transform;
        private Coroutine _growthCoroutine;
        private WaitForSeconds _waitCollectable;
        private WaitForSeconds _waitGrowthFirstPart;
        private WaitForSeconds _waitGrowthSecondPart;
        private GemInfo _gemInfo;
        
        private Ease _growthEase;
        private float _growthTime;
        private bool _isCollectable;
        
        public GemInfo GetGemInfo => _gemInfo;
        public Vector3 GetPosition => _transform.position;
        public float GetScale => _transform.localScale.x;
        public bool IsCollectable => _isCollectable;
        
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void OnEnable()
        {
            _growthEase = DataManager.Instance.GameData.GrowthEase;
            _growthTime = DataManager.Instance.GameData.GrowthTime;

            if (_growthTime < 0.25f)
            {
                _growthTime = 1f;
            }

            float collectableTime = 0.25f * _growthTime;
            _waitCollectable = new WaitForSeconds(collectableTime);
            _waitGrowthFirstPart = new WaitForSeconds(_growthTime * 0.85f - collectableTime);
            _waitGrowthSecondPart = new WaitForSeconds(_growthTime * 0.15f - collectableTime);
        }

        public override void Initiate(Vector3 position, Action<SpawnObject> callback)
        {
            _transform.position = position;
            completedAction = callback;
            _gemInfo = DataManager.Instance.GemData.GetRandomGemInfo();
            meshFilter.mesh = _gemInfo.Mesh;
            meshRenderer.material = _gemInfo.Material;
            meshRenderer.gameObject.SetActive(true);

            _transform.localScale = Vector3.zero;
            if (_growthCoroutine != null)
            {
                StopCoroutine(_growthCoroutine);
            }
            _growthCoroutine = StartCoroutine(GrowthCoroutine());
        }

        public override void CompleteTask()
        {
            if (_growthCoroutine != null)
            {
                StopCoroutine(_growthCoroutine);
                _growthCoroutine = null;
            }
            
            completedAction?.Invoke(this);
            meshRenderer.gameObject.SetActive(false);
            _isCollectable = false;
        }

        private IEnumerator GrowthCoroutine()
        {
            _transform.DOScale(Vector3.one * 0.9f, _growthTime * 0.85f);
            yield return _waitCollectable;
            _isCollectable = true;
            yield return _waitGrowthFirstPart;
            _transform.DOScale(Vector3.one, _growthTime * 0.25f).SetEase(_growthEase);
            yield return _waitGrowthSecondPart;
            onGrowthComplete?.Invoke();
            _growthCoroutine = null;
        }
    }
}
