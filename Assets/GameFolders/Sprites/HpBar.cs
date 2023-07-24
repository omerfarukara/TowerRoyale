using System;
using GameFolders.Scripts.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerRoyale.GameFolders.Sprites
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private Image hpBarImage;
        [SerializeField] private Gradient gradient;

        public float Health { get; set; }
        public float MaxHealth { get; set; }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
        }

        private void Update()
        {
            healthText.text = $"{Health} / {MaxHealth}";
            GradientColorHp(hpBarImage, Health, MaxHealth);
        }

        private void GradientColorHp(Image bar, float curHp, float maxHp)
        {
            hpBarImage.fillAmount = curHp / maxHp;
            bar.color = gradient.Evaluate(curHp / maxHp);
        }
    }
}