using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerRoyale
{
    public class ManaController : MonoSingleton<ManaController>
    {
        [SerializeField] private Slider fill;
        [SerializeField] private TextMeshProUGUI manaText;
        
        private EventData EventData => DataManager.Instance.EventData;

        private float _mana;

        public float Mana
        {
            get => _mana;
            set
            {
                if (value > 10.01) return;

                _mana = value;
                EventData.CardManaControl?.Invoke((int)value);
                manaText.text = ((int)value).ToString();
            }
        }

        private void Update()
        {
            Mana += Time.deltaTime / 2;
            fill.value = Mana / 10;
        }
    }
}