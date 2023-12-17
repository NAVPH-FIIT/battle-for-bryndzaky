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
using Bryndzaky.General.Common;
using Bryndzaky.Units.Player;

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
        // private List<string> activeWeapons;
        // private int skillpoints;
        private TextMeshProUGUI skillpointText = null;
  
        public void Start()
        {   
            gameObject.SetActive(false);
            // StateManager.State.skillpoints++;
            // PlayerPrefs.SetInt("skillpoints", 1); // TODO: Remove
            
            // this.InitializeStats();
            // this.skillpoints = PlayerPrefs.GetInt("skillpoints", 0);
            // this.skillpointText = transform.Find("SkillPointsText").GetComponent<TextMeshProUGUI>();
            

            // this.activeWeapons = PlayerPrefs.GetString("ActiveWeapons", "").Split('|').ToList();

            foreach (Transform statUpgrade in upgradeContainer.transform)
                this.statButtons.Add(this.SetupUpgrade(statUpgrade));
        }

        public void OnEnable()
        {
            this.skillpointText = this.skillpointText == null ? transform.Find("SkillPointsText").GetComponent<TextMeshProUGUI>() : this.skillpointText;
            this.skillpointText.text = this.skillpointText.text.Split(":")[0] + ": " + StateManager.State.skillpoints;

            foreach (Transform currentWeapon in this.weaponContainer.transform)
                Destroy(currentWeapon.gameObject);

            foreach (var weapon in armory.allWeapons)
            {
                int weaponGrade = StateManager.State.GetWeaponGrade(weapon.name);
                if (weaponGrade == -1)
                    continue;
                // int weaponGrade = PlayerPrefs.GetInt(weapon.name, -1);

                GameObject weaponObject = Instantiate(weaponPrefab);
                weaponObject.transform.SetParent(weaponContainer.transform);
                this.SetupWeapon(weapon, weaponObject, weaponGrade);
            }
        }

        public void Update()
        {
            foreach (Button button in this.statButtons)
                button.gameObject.SetActive(StateManager.State.skillpoints > 0);
        }

        private Button SetupUpgrade(Transform statUpgrade)
        {
            var statText = statUpgrade.GetComponentInChildren<TextMeshProUGUI>();
            string prefix = statText.text.Split(":")[0].Trim();
            string statId = prefix.ToLower().Replace(' ', '_').Trim();

            var stat = StateManager.State.stats.Find(s => s.name == statId);
            // int value = PlayerPrefs.GetInt(statId, -1);
            // switch (statId)
            // {
            //     case "max_health":
            //     {
            //         value = StateManager.State.maxHealth;
            //         // newValue = PlayerPrefs.GetInt(statId, 100) + 50;
            //         break;
            //     }
            //     case "move_speed":
            //     {
            //         value = StateManager.State.moveSpeed;
            //         // newValue = PlayerPrefs.GetInt(statId, 10) + 1;
            //         break;
            //     }
            //     case "dash_speed":
            //     {
            //         value = StateManager.State.dashSpeed;
            //         // newValue = PlayerPrefs.GetInt(statId, 10) + 1;
            //         break;
            //     }
            //     default:
            //     {
            //         value = -1;
            //         break;
            //     }
            // }
    	    statText.text = prefix + " : " + (stat == null ? -1 : stat.value);
            
            var upgradeButton = statUpgrade.GetComponentInChildren<Button>();
            
            upgradeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.AddListener(() => this.upgradeStat(statId, statUpgrade));

            return upgradeButton;
        }

        private void upgradeStat(string statId, Transform statUpgrade)
        {
            var stat = StateManager.State.stats.Find(s => s.name == statId);
            if (stat == null)
                return;
            stat.value += stat.increment;
            // int? newValue;
            // switch (statId)
            // {
            //     case "max_health":
            //     {
            //         StateManager.State.maxHealth += 50;
            //         // newValue = PlayerPrefs.GetInt(statId, 100) + 50;
            //         break;
            //     }
            //     case "move_speed":
            //     {
            //         StateManager.State.moveSpeed += 1;
            //         // newValue = PlayerPrefs.GetInt(statId, 10) + 1;
            //         break;
            //     }
            //     case "dash_speed":
            //     {
            //         StateManager.State.dashSpeed += 1;
            //         // newValue = PlayerPrefs.GetInt(statId, 10) + 1;
            //         break;
            //     }
            //     default:
            //     {
            //         return;
            //     }
            // }

            // if (newValue == null)
            //     return;

            // PlayerPrefs.SetInt(statId, (int) newValue);
            // PlayerPrefs.SetInt("skillpoints", --this.skillpoints);
            StateManager.State.skillpoints--;
            // PlayerPrefs.Save();
            this.skillpointText.text = this.skillpointText.text.Split(":")[0] + ": " + StateManager.State.skillpoints;
            Player.Instance.Initialize();
            this.SetupUpgrade(statUpgrade);
        }

        private void SetupWeapon(Armory.WeaponEntry weapon, GameObject weaponChoice, int weaponGrade)
        {
            Image weaponIcon  = weaponChoice.transform.Find("WeaponIcon").GetComponent<Image>();
            weaponIcon.sprite = weapon.prefab.GetComponentInChildren<SpriteRenderer>().sprite;
            
            TextMeshProUGUI weaponName  = weaponChoice.transform.Find("WeaponName").GetComponent<TextMeshProUGUI>();
            weaponName.text = weapon.name + ( weaponGrade > 0 ? " +" + weaponGrade : "");

            Toggle weaponToggle = weaponChoice.GetComponent<Toggle>();
            weaponToggle.onValueChanged.AddListener(isOn => this.ToggleWeapon(weaponToggle, isOn));


            #region Enable components
            foreach (Image img in weaponChoice.GetComponentsInChildren<Image>())
                img.enabled = true;
            weaponToggle.enabled = true;
            weaponName.enabled = true;
            #endregion

            if (StateManager.State.activeWeapons.Contains(weapon.name))
                weaponToggle.isOn = true;
        }

        private void ToggleWeapon(Toggle toggle, bool isOn)
        {
            // Transform upgrade = toggle.transform.parent;
            string weaponName = toggle.transform.Find("WeaponName").GetComponent<TextMeshProUGUI>().text.Split('+')[0].Trim();

            if (isOn)
            {
                if (StateManager.State.activeWeapons.Count >= 4)
                {
                    toggle.isOn = false;
                    return;
                }

                toggle.GetComponent<Image>().color = Color.green;
                
                if (!StateManager.State.activeWeapons.Contains(weaponName))
                {
                    StateManager.State.activeWeapons.Add(weaponName);
                    // PlayerPrefs.SetString("ActiveWeapons", string.Join("|", this.activeWeapons));
                }

                return;
            }

            if (StateManager.State.activeWeapons.Count == 1)
            {
                toggle.isOn = true;
                return;
            }
            toggle.GetComponent<Image>().color = new Color(1, 170f/255f, 0);
            StateManager.State.activeWeapons.Remove(weaponName);
            // PlayerPrefs.SetString("ActiveWeapons", string.Join("|", this.activeWeapons));
        }

        // private void InitializeStats()
        // {
        //     PlayerPrefs.SetInt("max_health", PlayerPrefs.GetInt("max_health", 100));
        //     PlayerPrefs.SetInt("move_speed", PlayerPrefs.GetInt("move_speed", 10));
        //     PlayerPrefs.SetInt("dash_speed", PlayerPrefs.GetInt("dash_speed", 10));
        //     PlayerPrefs.Save();
        // }
    }
}
