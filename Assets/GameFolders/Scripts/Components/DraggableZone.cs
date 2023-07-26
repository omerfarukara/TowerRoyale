using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General;
using UnityEngine;

namespace TowerRoyale
{
    public class DraggableZone : MonoBehaviour
    {
        [SerializeField] private Material draggableMaterial;
        [SerializeField] private bool isFlyZone;
        [SerializeField] private bool isExtraZone;
        
        private EventData EventData => DataManager.Instance.EventData;


        private Material _defaultMaterial;
        private MeshRenderer _meshRenderer;

        private void OnEnable()
        {
            EventData.OnDraggableZone += DraggableMaterial;
        }

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _defaultMaterial = _meshRenderer.sharedMaterials[0];
        }

        private void OnDisable()
        {
            EventData.OnDraggableZone -= DraggableMaterial;
        }

        private void DraggableMaterial(CharacterData characterData, bool status)
        {
            if (gameObject.layer != LayerMask.NameToLayer("PlayerSpawnPoint")) return;
            if (isFlyZone && characterData.nickname != "Dragon") return;
            _meshRenderer.material = status ? draggableMaterial : _defaultMaterial;
            Vector3 position = transform.position;
            if (isExtraZone)
            {
                _meshRenderer.enabled = status;
            }
            transform.position = status
                ? new Vector3(position.x, 0.01f, position.z)
                : new Vector3(position.x, 0f, position.z);
        }
    }
}