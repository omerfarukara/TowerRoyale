using System.Collections.Generic;
using GameFolders.Scripts.General;
using TowerRoyale;
using UnityEngine;

namespace GameFolders.Scripts.Controllers
{
    public class GameController : MonoSingleton<GameController>
    {
        [SerializeField] internal List<Transform> playerTowers;
        [SerializeField] internal List<Transform> aiTowers;
        
        internal List<SpawnObject> _spawnedAi = new List<SpawnObject>();
        internal List<SpawnObject> _spawnedPlayer = new List<SpawnObject>();
        
    }
}
