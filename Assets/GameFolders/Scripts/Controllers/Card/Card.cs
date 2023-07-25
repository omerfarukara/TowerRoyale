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

        [SerializeField] private List<Image> alphaImages;

        //private SpawnObject SpawnObject { get; set; }
        public CharacterData CharacterData { get; set; }

        private EventData EventData => DataManager.Instance.EventData;

        private bool _isActive;

        public bool IsActive
        {
            get => _isActive;
            set { _isActive = value; }
        }

        private void Awake()
        {
            SetCharacterData();
        }

        private void OnEnable()
        {
            EventData.CardManaControl += CardControl;
        }

        private void OnDisable()
        {
            EventData.CardManaControl -= CardControl;
        }

        internal void OnSpawn(Vector3 pos)
        {
            EventData.OnSpawnCharacter?.Invoke(characterType, pos);
        }

        private void SetCharacterData()
        {
            switch (characterType)
            {
                case CharacterType.Knight:
                    CharacterData = DataManager.Instance.KnightData;
                    break;
                case CharacterType.Archer:
                    CharacterData = DataManager.Instance.ArcherData;
                    break;
                case CharacterType.Dragon:
                    CharacterData = DataManager.Instance.DragonData;
                    break;
                case CharacterType.Support:
                    CharacterData = DataManager.Instance.SupportData;
                    break;
            }

            SetVariablesByData();
        }

        private void SetVariablesByData()
        {
            characterNameText.text = CharacterData.nickname;
            characterImage.sprite = CharacterData.characterSprite;
            manaText.text = CharacterData.mana.ToString();
        }

        private void CardControl(int mana)
        {
            if (mana >= CharacterData.mana)
            {
                CardActive();
            }
            else
            {
                CardDeActive();
            }
        }

        private void CardDeActive()
        {
            foreach (var aImages in alphaImages)
            {
                aImages.color = new Color(aImages.color.r, aImages.color.g, aImages.color.b, 177 / 255f);
            }

            IsActive = false;
        }

        private void CardActive()
        {
            foreach (var aImages in alphaImages)
            {
                aImages.color = new Color(aImages.color.r, aImages.color.g, aImages.color.b, 255 / 255f);
            }

            IsActive = true;
        }
    }
}