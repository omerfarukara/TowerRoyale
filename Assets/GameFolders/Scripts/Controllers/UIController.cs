using System;
using DG.Tweening;
using GameFolders.Scripts.General;
using GameFolders.Scripts.Managers;
using TowerRoyale;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace GameFolders.Scripts.Controllers
{
    public class UIController : MonoSingleton<UIController>
    {
        private EventData _eventData => DataManager.Instance.EventData;
        

        [Header("Panels")]
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private GameObject losePanel;
    
        [Header("Buttons")]
        [SerializeField] Button nextLevelButton;
        [SerializeField] Button tryAgainButton;

        private void OnEnable()
        {
            nextLevelButton.onClick.AddListener(OnNextLevel);
            tryAgainButton.onClick.AddListener(OnTryAgain);
            _eventData.OnFinishLevel += OnFinish;
        }

        private void OnDisable()
        {
            _eventData.OnFinishLevel -= OnFinish;
        }

        private void OnFinish(bool status)
        {
            if (status)
            {
                victoryPanel.SetActive(true);
                victoryPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
            }
            else
            {
                losePanel.SetActive(true);
                losePanel.transform.DOScale(Vector3.one, 0.5f);
            }
        }

        private void OnNextLevel()
        {
            //GameManager.Instance.NextLevel();
            Addressables.Release(GameManager.Instance._sceneHandle);
            SceneManager.LoadScene("MainMenu");
        }

        private void OnTryAgain()
        {
            //GameManager.Instance.TryAgain();
            Addressables.Release(GameManager.Instance._sceneHandle);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
