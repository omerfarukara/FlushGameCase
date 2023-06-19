using System;
using DG.Tweening;
using FlushGameCase.Core;
using UnityEngine;

namespace FlushGameCase.Game
{
    public class CollectedGem : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshRenderer meshRenderer;


        private Transform _transform;
        private GemInfo _gemInfo;
        private Tween _collectTween;
        private Action<CollectedGem> _callback;
        
        private Ease _collectEase;
        private float _collectTime;

        public GemInfo GemInfo => _gemInfo;

        private void Awake()
        {
            _transform = GetComponent<Transform>(); // For optimization ***
        }

        private void OnEnable()
        {
            _collectEase = DataManager.Instance.GameData.CollectEase;
            _collectTime = DataManager.Instance.GameData.CollectTime;
        }

        public void Collect(GemInfo gemInfo, float scale, Vector3 startPosition, Vector3 endPosition, Action<CollectedGem> callback, bool autoComplete = false)
        {
            _gemInfo = gemInfo;
            meshFilter.mesh = _gemInfo.Mesh;
            meshRenderer.material = _gemInfo.Material;
            meshRenderer.gameObject.SetActive(true);
            _transform.localScale = Vector3.one * scale;
            _transform.position = startPosition;
            if (_collectTween != null)
            {
                if (_collectTween.IsPlaying())
                {
                    _collectTween.Kill();
                }
            }
            _collectTween = _transform.DOLocalMove(endPosition, _collectTime).SetEase(_collectEase).OnComplete(() =>
            {
                if (autoComplete)
                {
                    CompleteTask();
                }
            });
            _callback = callback;
        }

        public void CompleteTask()
        {
            if (_collectTween != null)
            {
                if (_collectTween.IsPlaying())
                {
                    _collectTween.Kill();
                }
            }
            _callback?.Invoke(this);
            meshRenderer.gameObject.SetActive(false);
        }
    }
}
