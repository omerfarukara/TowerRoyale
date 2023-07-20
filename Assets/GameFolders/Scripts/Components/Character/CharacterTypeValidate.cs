using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General;
using GameFolders.Scripts.General.Enum;
using UnityEngine;

namespace TowerRoyale
{
    public class CharacterTypeValidate : MonoBehaviour
    {
        [SerializeField] private CharacterType type;
        private EventData EventData => DataManager.Instance.EventData;

        private void OnEnable()
        {
            EventData.CharacterValidate += DataValidate;
        }

        private void DataValidate(CharacterType obj)
        {
            transform.localScale = obj == type ? Vector3.one * 4 : Vector3.zero;
            EventData.CharacterValidate -= DataValidate;
        }
    }
}