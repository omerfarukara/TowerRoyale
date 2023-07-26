using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameFolders.Scripts.General;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerRoyale
{
    public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private LayerMask layer;
        [SerializeField] private Image image;
        [SerializeField] private Card card;

        private Transform _transform;
        private Transform _parentAfterDrag;
        private Vector3 _defaultPosition;
        private Camera _camera;
        private EventData EventData => DataManager.Instance.EventData;

        private void Awake()
        {
            _transform = transform;
            _camera = Camera.main;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!card.IsActive) return;

            _parentAfterDrag = _transform.parent;
            _defaultPosition = _transform.position;
            _transform.SetParent(_transform.root);
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!card.IsActive) return;
            EventData.OnDraggableZone?.Invoke(card.CharacterData,true);

            _transform.position = Input.mousePosition;
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!card.IsActive) return;

            _transform.SetParent(_parentAfterDrag);
            image.raycastTarget = false;
            EventData.OnDraggableZone?.Invoke(card.CharacterData,false);


            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer)) // Layer'a çevirilcek
            {
                card.OnSpawn(hit.point);
                ManaController.Instance.Mana -= card.CharacterData.mana;
                _transform.DOMove(_defaultPosition, 0.2f);
                image.raycastTarget = true;
            }
            else
            {
                _transform.position = _defaultPosition;
                image.raycastTarget = true;
            }
        }
    }
}