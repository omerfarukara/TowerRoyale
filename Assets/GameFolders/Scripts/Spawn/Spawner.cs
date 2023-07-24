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
        [SerializeField] private OwnerType ownerType;

        [SerializeField] private SpawnObject archerObject;
        [SerializeField] private SpawnObject knightObject;
        [SerializeField] private SpawnObject dragonObject;
        [SerializeField] private SpawnObject supportObject;

        private Queue<SpawnObject> _spawnObjects = new Queue<SpawnObject>();
        private EventData EventData => DataManager.Instance.EventData;

        private void OnEnable()
        {
            if (ownerType == OwnerType.Enemy) return;
            EventData.OnSpawnCharacter += Spawn;
        }

        private void OnDisable()
        {
            if (ownerType == OwnerType.Enemy) return;
            EventData.OnSpawnCharacter -= Spawn;
        }

        private void Spawn(CharacterType characterType, Vector3 pos)
        {
            if (_spawnObjects.Count == 0)
            {
                SpawnObject instantiateObject = characterType switch
                {
                    CharacterType.Knight => Instantiate(knightObject, pos, Quaternion.identity),
                    CharacterType.Archer => Instantiate(archerObject, pos, Quaternion.identity),
                    CharacterType.Dragon => Instantiate(dragonObject, pos, Quaternion.identity),
                    CharacterType.Support => Instantiate(supportObject, pos, Quaternion.identity),
                    _ => null
                };

                _spawnObjects.Enqueue(instantiateObject);
            }

            SpawnObject currentObject = _spawnObjects.Dequeue();
            currentObject.Initialize(ReturnToQueue, ownerType, pos);
        }
        
        internal void Spawn(CharacterType characterType, Vector3 pos,Transform parent)
        {
            if (_spawnObjects.Count == 0)
            {
                SpawnObject instantiateObject = characterType switch
                {
                    CharacterType.Knight => Instantiate(knightObject, pos, Quaternion.identity,parent),
                    CharacterType.Archer => Instantiate(archerObject, pos, Quaternion.identity,parent),
                    CharacterType.Dragon => Instantiate(dragonObject, pos, Quaternion.identity,parent),
                    CharacterType.Support => Instantiate(supportObject, pos, Quaternion.identity,parent),
                    _ => null
                };

                _spawnObjects.Enqueue(instantiateObject);
            }

            SpawnObject currentObject = _spawnObjects.Dequeue();
            currentObject.Initialize(ReturnToQueue, ownerType, pos);
        }

        private void ReturnToQueue(SpawnObject spawnObject)
        {
            _spawnObjects.Enqueue(spawnObject);
        }
    }
}