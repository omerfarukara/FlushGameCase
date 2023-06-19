using System;
using System.Collections.Generic;
using DG.Tweening;
using FlushGameCase.Core;
using FlushGameCase.Game;
using UnityEngine;
using UnityEngine.UI;

namespace FlushGameCase.UI
{
    public class UIGemController : MonoBehaviour
    {
        [SerializeField] private UIGem uiGemPrefab;
        [SerializeField] private Button openPanelButton;
        [SerializeField] private Button closePanelButton;
        [SerializeField] private GameObject gemPanel;
        [SerializeField] private Transform content;
        
        private List<UIGem> _uiGems = new List<UIGem>();

        private Tween _openTween;
        private Tween _closeTween;

        private void Awake()
        {
            openPanelButton.onClick.AddListener(OpenPanel);
            closePanelButton.onClick.AddListener(ClosePanel);
        }

        private void Start()
        {
            gemPanel.transform.localScale = Vector3.zero;
        }

        private void OpenPanel()
        {
            openPanelButton.onClick.RemoveAllListeners();
            openPanelButton.onClick.AddListener(ClosePanel);
            gemPanel.transform.localScale = Vector3.zero;

            if (_openTween != null)
            {
                if (_openTween.IsPlaying())
                {
                    _openTween.Kill();
                }
            }
            
            if (_closeTween != null)
            {
                if (_closeTween.IsPlaying())
                {
                    _closeTween.Kill();
                }
            }
            
            _openTween = gemPanel.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutQuint);

            GemInfo[] gemInfos = DataManager.Instance.GemData.GetAllGems();

            for (int i = 0; i < gemInfos.Length; i++)
            {
                UIGem uiGem = null;
                
                if (_uiGems.Count > i)
                {
                    uiGem = _uiGems[i];
                }
                else
                {
                    uiGem = Instantiate(uiGemPrefab, content);
                    _uiGems.Add(uiGem);
                }
                
                uiGem.Initialize(gemInfos[i]);
            }
        }

        private void ClosePanel()
        {
            if (_openTween != null)
            {
                if (_openTween.IsPlaying())
                {
                    _openTween.Kill();
                }
            }
            
            if (_closeTween != null)
            {
                if (_closeTween.IsPlaying())
                {
                    _closeTween.Kill();
                }
            }
            
            _closeTween = gemPanel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.Flash).OnComplete(() =>
            {
                openPanelButton.onClick.RemoveAllListeners();
                openPanelButton.onClick.AddListener(OpenPanel);
            });
        }
    }
}
