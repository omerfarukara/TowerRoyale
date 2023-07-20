using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General;
using GameFolders.Scripts.General.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TowerRoyale
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnObject archerObject;
        [SerializeField] private SpawnObject knightObject;

        private Queue<SpawnObject> _spawnObjects = new Queue<SpawnObject>();
        private EventData EventData => DataManager.Instance.EventData;

        private void OnEnable()
        {
            EventData.OnSpawnCharacter += Spawn;
        }

        private void OnDisable()
        {
            EventData.OnSpawnCharacter -= Spawn;
        }

        private void Spawn(CharacterType characterType, Vector3 pos)
        {
            if (_spawnObjects.Count == 0)
            {
                SpawnObject instantiateObject = null;
                switch (characterType)
                {
                    case CharacterType.Knight:
                        instantiateObject = Instantiate(knightObject, pos, Quaternion.identity);
                        break;
                    case CharacterType.Archer:
                        instantiateObject = Instantiate(archerObject, pos, Quaternion.identity);
                        break;
                }

                _spawnObjects.Enqueue(instantiateObject);
            }

            SpawnObject currentObject = _spawnObjects.Dequeue();
            currentObject.Initialize(characterType, ReturnToQueue);
        }

        private void ReturnToQueue(SpawnObject spawnObject)
        {
            _spawnObjects.Enqueue(spawnObject);
        }
    }
}