using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General;
using GameFolders.Scripts.General.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerRoyale
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private CharacterType characterType;
        [SerializeField] private TextMeshProUGUI characterNameText;
        [SerializeField] private TextMeshProUGUI manaText;
        [SerializeField] private Image characterImage;

        //private SpawnObject SpawnObject { get; set; }
        private CharacterData _characterData;
        private EventData EventData => DataManager.Instance.EventData;

        private void Awake()
        {
            SetCharacterData();
        }

        internal void OnSpawn(Vector3 pos)
        {
            EventData.OnSpawnCharacter?.Invoke(characterType,pos);
        }

        private void SetCharacterData()
        {
            switch (characterType)
            {
                case CharacterType.Knight:
                    _characterData = DataManager.Instance.KnightData;
                    break;
                case CharacterType.Archer:
                    _characterData = DataManager.Instance.ArcherData;
                    break;
            }
            SetVariablesByData();
        }
        private void SetVariablesByData()
        {
            characterNameText.text = _characterData.nickname;
            characterImage.sprite = _characterData.characterSprite;
            manaText.text = _characterData.mana.ToString();
        }
    }
}
