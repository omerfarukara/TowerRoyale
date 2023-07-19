using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TowerRoyale
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform refSpawnPoint;

        Queue<SpawnObject> _spawnObjects = new Queue<SpawnObject>();


        [Button]
        internal void Spawn(SpawnObject spawnObject)
        {
            if (_spawnObjects.Count == 0)
            {
                SpawnObject instantiateObject = Instantiate(spawnObject, refSpawnPoint);
                _spawnObjects.Enqueue(instantiateObject);
            }

            SpawnObject currentObject = _spawnObjects.Dequeue();
            currentObject.Initialize(ReturnToQueue);
        }

        private void ReturnToQueue(SpawnObject spawnObject)
        {
            _spawnObjects.Enqueue(spawnObject);
        }
    }
}