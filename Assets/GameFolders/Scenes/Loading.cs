using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameFolders.Scripts.General;
using GameFolders.Scripts.Managers;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using UnityEngine.UI;

namespace TowerRoyale
{
    public class Loading : MonoSingleton<Loading>
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI loadingPercentage;
        [SerializeField] private string levelName;
        [SerializeField] private int loadLevelDelayTime;

        [SerializeField] private GameObject downloadSizePanel;
        [SerializeField] private TextMeshProUGUI downloadSizeText;

        private bool _isWaiting;

        private void OnDisable()
        {
            GameManager.Instance._sceneHandle.Completed -= OnSceneLoaded;
        }

        private void GoToNextLevel()
        {
            Addressables.LoadSceneAsync(levelName, UnityEngine.SceneManagement.LoadSceneMode.Single, true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                CacheRemove();
            }
            
            if (!_isWaiting) return;

            loadingPercentage.text = $"{GameManager.Instance._sceneHandle.PercentComplete * 100}%";
            slider.value = GameManager.Instance._sceneHandle.PercentComplete;
        }

        public void SceneAsyncLoad()
        {
            downloadSizePanel.SetActive(false);
            
            slider.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
            {
                GameManager.Instance._sceneHandle = Addressables.DownloadDependenciesAsync(levelName);
                GameManager.Instance._sceneHandle.Completed += OnSceneLoaded;

                _isWaiting = true;
            }).SetDelay(0.5f);
        }

        private void OnSceneLoaded(AsyncOperationHandle obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                Invoke(nameof(GoToNextLevel), loadLevelDelayTime);
            }
        }

        private void CacheRemove()
        {
            Caching.ClearCache();
        }

        public void ShowDownloadSize()
        {
            StartCoroutine(DownloadSize());
        }

        IEnumerator DownloadSize()
        {
            var downloadSize = Addressables.GetDownloadSizeAsync(levelName);
            yield return downloadSize;
            if (downloadSize.Result >0)
            {
                downloadSizeText.text = $"Download {downloadSize.Result / 1024f / 1024f:0.00} MB ?";
                downloadSizePanel.SetActive(true);
            }
            else
            {
                SceneAsyncLoad();
            }
        }
    }
}