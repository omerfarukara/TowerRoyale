using System;
using System.Collections.Generic;
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

        public List<Transform> GetTowers(OwnerType ownerType)
        {
            return ownerType switch
            {
                OwnerType.Player => enemyTowers,
                OwnerType.Enemy => playerTowers
            };
        }
    }
}
