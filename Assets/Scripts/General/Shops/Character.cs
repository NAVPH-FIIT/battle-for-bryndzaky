using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Bryndzaky.Combat.Weapons;
using Bryndzaky.Combat.Spells;
using System;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System.Runtime.ConstrainedExecution;

namespace Bryndzaky.Hub
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Armory armory;
        [SerializeField] private GameObject weaponPrefab;
        [SerializeField] private GameObject upgradeContainer;
        [SerializeField] private GameObject weaponContainer;
        private List<Button> statButtons = new();
        private List<TextMeshProUGUI> statTexts = new();
        private List<string> activeWeapons;
        private int skillpoints;
        private TextMeshProUGUI skillpointText;
  

        public void Start()
        {   
            PlayerPrefs.SetInt("skillpoints", 1); // TODO: Remove
            this.InitializeStats();
            this.skillpoints = PlayerPrefs.GetInt("skillpoints", 0);
            this.skillpointText = transform.Find("SkillPointsText").GetComponent<TextMeshProUGUI>();
            this.skillpointText.text = this.skillpointText.text.Split(":")[0] + ": " + this.skillpoints;

            this.activeWeapons = PlayerPrefs.GetString("ActiveWeapons", "").Split('|').ToList();

            foreach (var weapon in armory.allWeapons)
            {
                int weaponGrade = PlayerPrefs.GetInt(weapon.name, -1);
                if (weaponGrade == -1)
                    continue;

                GameObject weaponObject = Instantiate(weaponPrefab);
                weaponObject.transform.SetParent(weaponContainer.transform);
                this.SetupWeapon(weapon, weaponObject, weaponGrade);
            }

            foreach (Transform statUpgrade in upgradeContainer.transform)
            {
                this.statButtons.Add(this.SetupUpgrade(statUpgrade));
            }
        }

        public void Update()
        {
            foreach (Button button in this.statButtons)
                button.gameObject.SetActive(this.skillpoints > 0);
        }

        private Button SetupUpgrade(Transform statUpgrade)
        {
            var statText = statUpgrade.GetComponentInChildren<TextMeshProUGUI>();
            string prefix = statText.text.Split(":")[0];
            string statId = prefix.ToLower().Replace(' ', '_').Trim();
            int value = PlayerPrefs.GetInt(statId, -1);
            
    	    statText.text = prefix + ": " + value;
            
            var upgradeButton = statUpgrade.GetComponentInChildren<Button>();
            
            upgradeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.AddListener(() => this.upgradeStat(statId, statUpgrade));

            return upgradeButton;
        }

        private void upgradeStat(string statId, Transform statUpgrade)
        {
            int? newValue;
            switch (statId)
            {
                case "max_health":
                {
                    newValue = PlayerPrefs.GetInt(statId, 100) + 50;
                    break;
                }
                case "move_speed":
                {
                    newValue = PlayerPrefs.GetInt(statId, 10) + 1;
                    break;
                }
                case "dash_speed":
                {
                    newValue = PlayerPrefs.GetInt(statId, 10) + 1;
                    break;
                }
                default:
                {
                    newValue = null;
                    break;
                }
            }

            if (newValue == null)
                return;

            PlayerPrefs.SetInt(statId, (int) newValue);
            PlayerPrefs.SetInt("skillpoints", --this.skillpoints);
            PlayerPrefs.Save();
            this.skillpointText.text = this.skillpointText.text.Split(":")[0] + ": " + this.skillpoints;
            this.SetupUpgrade(statUpgrade);
        }

        private void SetupWeapon(Armory.WeaponEntry weapon, GameObject weaponChoice, int weaponGrade)
        {
            Image weaponIcon  = weaponChoice.transform.Find("WeaponIcon").GetComponent<Image>();
            weaponIcon.sprite = weapon.prefab.GetComponentInChildren<SpriteRenderer>().sprite;
            
            TextMeshProUGUI weaponName  = weaponChoice.transform.Find("WeaponName").GetComponent<TextMeshProUGUI>();
            weaponName.text = weapon.name + ( weaponGrade > 0 ? " +" + weaponGrade : "");
            
            Toggle weaponToggle = weaponChoice.GetComponentInChildren<Toggle>();

            weaponToggle.onValueChanged.AddListener(isOn => this.ToggleWeapon(weaponToggle, isOn));

                if (this.activeWeapons.Contains(weapon.name))
                    weaponToggle.isOn = true;
        }

        private void ToggleWeapon(Toggle toggle, bool isOn)
        {
            // Transform upgrade = toggle.transform.parent;
            string weaponName = toggle.transform.Find("WeaponName").GetComponent<TextMeshProUGUI>().text.Split('+')[0].Trim();

            if (isOn)
            {
                if (this.activeWeapons.Count >= 4)
                {
                    toggle.isOn = false;
                    return;
                }

                toggle.GetComponent<Image>().color = Color.green;
                
                if (!this.activeWeapons.Contains(weaponName))
                {
                    this.activeWeapons.Add(weaponName);
                    PlayerPrefs.SetString("ActiveWeapons", string.Join("|", this.activeWeapons));
                }

                return;
            }

            toggle.GetComponent<Image>().color = new Color(1, 170f/255f, 0);
            this.activeWeapons.Remove(weaponName);
            PlayerPrefs.SetString("ActiveWeapons", string.Join("|", this.activeWeapons));
        }

        private void InitializeStats()
        {
            PlayerPrefs.SetInt("max_health", PlayerPrefs.GetInt("max_health", 100));
            PlayerPrefs.SetInt("move_speed", PlayerPrefs.GetInt("move_speed", 10));
            PlayerPrefs.SetInt("dash_speed", PlayerPrefs.GetInt("dash_speed", 10));
            PlayerPrefs.Save();
        }
    }
}
