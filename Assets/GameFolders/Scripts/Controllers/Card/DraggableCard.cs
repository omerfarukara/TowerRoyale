using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerRoyale
{
    public class DraggableCard : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
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
            _parentAfterDrag = _transform.parent;
            _defaultPosition = _transform.position;
            _transform.SetParent(_transform.root);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _transform.SetParent(_parentAfterDrag);
            image.raycastTarget = false;
            
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out hit, Mathf.Infinity,layer)) // Layer'a çevirilcek
            {
                card.OnSpawn(hit.point);
                gameObject.SetActive(false);
            }
            else
            {
                _transform.position = _defaultPosition;
                image.raycastTarget = true;
            }
        }
    }
}
