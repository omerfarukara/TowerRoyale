using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameFolders.Scripts.Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerRoyale
{
    public class Logo : MonoBehaviour
    {
        [SerializeField] private Transform logoText;
        [SerializeField] private Transform logo;

        [SerializeField] private float tweenDuration;
        [SerializeField] private Ease ease;

        private void Start()
        {
            logo.DOScale(Vector3.one, tweenDuration).SetEase(ease);
            logoText.DOScale(Vector3.one, tweenDuration).SetDelay(tweenDuration / 2).SetEase(ease).OnComplete(() =>
            {
                GameManager.Instance.LoadSceneAfterDelay();
            });
        }
    }
}