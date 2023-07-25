using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Controllers;
using GameFolders.Scripts.General.Enum;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerRoyale
{
    public class AISpawnerController : MonoBehaviour
    {
        [SerializeField] private List<Spawner> spawner;
        [SerializeField] private Vector3 initiatePosition;

        [SerializeField] private float defaultEverySecond;
        [SerializeField] private float decreaseEverySecond;

        [SerializeField] private float level1Time;
        [SerializeField] private int level2Time;
        [SerializeField] private int level3Time;
        [SerializeField] private int level4Time;

        private float _timer;
        private float _everySecond;

        private void Awake()
        {
            _everySecond = defaultEverySecond;
        }

        private void Start()
        {
            Invoke(nameof(SpawnCharacterByTimer), _everySecond);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        private void SpawnCharacterByTimer()
        {
            if (_timer < level1Time)
            {
                SpawnCharacter(CharacterType.Knight);
            }
            else if (_timer < level2Time && _timer > level1Time)
            {
                SpawnCharacter(CharacterType.Archer);
            }
            else if (_timer < level3Time && _timer > level2Time)
            {
                SpawnCharacter(CharacterType.Support);
            }
            else if (_timer < level4Time && _timer > level3Time)
            {
                SpawnCharacter(CharacterType.Knight);
            }
            else
            {
                SpawnCharacter(CharacterType.Archer);
            }

            _everySecond = _everySecond - decreaseEverySecond < 0.5f ? 0.5f : _everySecond - decreaseEverySecond;
            Invoke(nameof(SpawnCharacterByTimer), _everySecond);
        }

        private void SpawnCharacter(CharacterType type)
        {
            int index = Random.Range(0, GameController.Instance.enemyTowers.Count);
            Vector3 pos = spawner[index].transform.position + initiatePosition;
            spawner[index].Spawn(type, pos, spawner[index].transform);
        }
    }
}