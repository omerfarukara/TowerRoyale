using System;
using System.Collections.Generic;
using System.Linq;
using GameFolders.Scripts.General;
using GameFolders.Scripts.General.Enum;
using TowerRoyale;
using UnityEngine;

namespace GameFolders.Scripts.Controllers
{
    public class GameController : MonoSingleton<GameController>
    {
        [SerializeField] private List<Transform> playerTowers;
        [SerializeField] private List<Transform> enemyTowers;

        private EventData EventData => DataManager.Instance.EventData;

        private void CheckTowerGameControl()
        {
            if (playerTowers.Count == 0)
            {
                EventData.OnFinishLevel?.Invoke(false);
            }
            else if (enemyTowers.Count == 0)
            {
                EventData.OnFinishLevel?.Invoke(true);
            }
        }

        public List<Transform> GetTowers(OwnerType ownerType)
        {
            return ownerType switch
            {
                OwnerType.Player => enemyTowers,
                OwnerType.Enemy => playerTowers
            };
        }

        public void RemoveTower(Transform _transform)
        {
            Transform playerTower = null;
            foreach (var tower in playerTowers)
            {
                if (tower == _transform)
                {
                    playerTower = tower;
                }
            }

            if (playerTower != null) playerTowers.Remove(playerTower);


            Transform enemyTower = null;
            foreach (var tower in enemyTowers)
            {
                if (tower == _transform)
                {
                    enemyTower = tower;
                }
            }

            if (enemyTower != null) enemyTowers.Remove(enemyTower);
            
            CheckTowerGameControl();
        }
    }
}