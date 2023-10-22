using System.Collections;
using System.Collections.Generic;
using Bryndzaky.General.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Bryndzaky.Units.Enemies
{
    public class Healthbar : MonoBehaviour
    {
        private Slider slider;
        [SerializeField]
        private Image img;
        private Enemy enemy;
        private float maxhealth;
        private int health;
        private void Start()
        {
            slider = GetComponent<Slider>();
            enemy = GetComponentInParent<Enemy>();
            maxhealth = enemy.GetMaxHealth();
            UpdateHealthBar();
        }
        public void UpdateHealthBar()
        {
            health = enemy.GetHealth();
            slider.value = health/maxhealth;
            CheckColor();
        }

        private void CheckColor()
        {
            if ((health / maxhealth) >= 0.75) 
            {
                img.color = new Color(32/255f, 255 / 255f, 0 / 255f, 255 / 255f);
                return;
            }
            if (0.25 <= (health / maxhealth) && (health / maxhealth) < 0.75)
            {
                img.color = new Color(255 / 255f, 146 / 255f, 0 / 255f, 255 / 255f);
                return;
            }
            if (health / maxhealth < 0.25)
            {
                img.color = new Color(255 / 255f, 0 / 255f, 5 / 255f, 255 / 255f);
                return;
            }
        }
    }
}
