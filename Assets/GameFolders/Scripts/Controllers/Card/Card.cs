using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private Image characterImage;

        internal SpawnObject SpawnObject { get; set; }
        private CharacterData _characterData;

        private void Awake()
        {
            SetCharacterData();
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
            SpawnObject = _characterData.spawnObject;
        }
    }
}
