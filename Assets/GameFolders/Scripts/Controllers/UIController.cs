using System;
using GameFolders.Scripts.General;
using GameFolders.Scripts.Managers;
using TowerRoyale;
using UnityEngine;
using UnityEngine.UI;

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
            victoryPanel.SetActive(status);
            losePanel.SetActive(!status);
        }

        private void OnNextLevel()
        {
            GameManager.Instance.NextLevel();
        }

        private void OnTryAgain()
        {
            GameManager.Instance.TryAgain();
        }
    }
}
