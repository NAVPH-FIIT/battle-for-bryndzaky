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

namespace Bryndzaky.Hub
{
    public class Blacksmith : MonoBehaviour
    {
        [SerializeField] private Armory armory;
        [SerializeField] private GameObject upgradePrefab;
        [SerializeField] private GameObject upgradeContainer;
        private List<Button> purchaseButtons = new();
        private List<TextMeshProUGUI> priceTexts = new();
        // private List<string> activeWeapons;

        public void Start()
        {
            gameObject.SetActive(false);
            foreach (Transform weapon in upgradeContainer.transform)
                Destroy(weapon.gameObject);
            // PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold", 100));
            // PlayerPrefs.SetInt("gold", 1000); // TODO: Remove

            // this.activeWeapons = PlayerPrefs.GetString("ActiveWeapons", "").Split('|').ToList();

            foreach (var weapon in armory.allWeapons)
            {
                GameObject upgrade = Instantiate(upgradePrefab);
                upgrade.transform.SetParent(upgradeContainer.transform);
                this.SetupUpgrade(weapon, upgrade);
            }
        }

        public void Update()
        {
            // int playerGold = PlayerPrefs.GetInt("gold", 0);
            for (int i = 0; i < this.purchaseButtons.Count; i++)
                if (int.Parse(this.priceTexts[i].text.Split(' ')[0]) > StateManager.State.gold)
                {
                    this.priceTexts[i].color = Color.red;
                    this.purchaseButtons[i].interactable = false;
                }
                else
                {
                    this.priceTexts[i].color = Color.white;
                    this.purchaseButtons[i].interactable = true;
                }
        }

        private void SetupUpgrade(Armory.WeaponEntry weapon, GameObject upgrade)
        {
            
            int weaponGrade = StateManager.State.GetWeaponGrade(weapon.name);
            
            Image weaponIcon            = upgrade.transform.Find("WeaponIcon").GetComponent<Image>();
            TextMeshProUGUI weaponName  = upgrade.transform.Find("WeaponName").GetComponent<TextMeshProUGUI>();
            GameObject upgradeUI        = upgrade.transform.Find("UpgradeUI").gameObject;
            TextMeshProUGUI priceText   = upgradeUI.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI buttonText  = upgradeUI.transform.Find("PurchaseButton").GetComponentInChildren<TextMeshProUGUI>();
            Button button = upgrade.GetComponentInChildren<Button>();
            button.onClick.RemoveAllListeners();
            weaponIcon.sprite = weapon.prefab.GetComponentInChildren<SpriteRenderer>().sprite;
            // Toggle weaponToggle = upgrade.GetComponentInChildren<Toggle>();
            // weaponToggle.onValueChanged.RemoveAllListeners();

            #region Enable components
            foreach (Image img in upgrade.GetComponentsInChildren<Image>())
                img.enabled = true;
            // weaponToggle.enabled = true;
            weaponName.enabled = true;
            #endregion

            if (this.purchaseButtons.Count != this.armory.allWeapons.Count)
            {
                this.purchaseButtons.Add(button);
                this.priceTexts.Add(priceText);
            }

            if (weaponGrade == -1)
            {
                weaponName.text = weapon.name;
                buttonText.text = "Purchase";
                priceText.text = weapon.upgrades[0].price.ToString() + " G";
                button.onClick.AddListener(() => this.PurchaseWeapon(weapon, upgrade, weapon.upgrades[0].price));

                // weaponToggle.enabled = false;
            }
            else
            {
                weaponName.text = weapon.name + (weaponGrade > 0 ? " +" + weaponGrade : "");
                
                if (weaponGrade == weapon.upgrades.Count - 1)
                {
                    upgradeUI.SetActive(false);
                    return;
                }
                
                buttonText.text = "Upgrade";
                priceText.text = weapon.upgrades[weaponGrade + 1].price.ToString() + " G";

                button.onClick.AddListener(() => this.UpgradeWeapon(weapon, upgrade, weapon.upgrades[weaponGrade + 1].price));

                // weaponToggle.enabled = true;
                // weaponToggle.onValueChanged.AddListener(isOn => this.ToggleWeapon(weaponToggle, isOn));

                // if (StateManager.State.activeWeapons.Contains(weapon.name))
                //     weaponToggle.isOn = true;
            }
        }

        private void UpgradeWeapon(Armory.WeaponEntry weapon, GameObject upgrade, int price)
        {
            StateManager.State.availableWeapons.Find(w => w.name == weapon.name).grade++;
            // PlayerPrefs.SetInt(weapon.name, PlayerPrefs.GetInt(weapon.name, 0) + 1);
            StateManager.State.gold -= price;
            // PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") - price);
            // PlayerPrefs.Save();
            this.SetupUpgrade(weapon, upgrade);
        }

        private void PurchaseWeapon(Armory.WeaponEntry weapon, GameObject upgrade, int price)
        {
            StateManager.State.availableWeapons.Add(new StateManager.GameState.AvailableWeapon{ name=weapon.name, grade=0 });
            // PlayerPrefs.SetInt(weapon.name, 0);
            StateManager.State.gold -= price;
            // PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") - price);
            // PlayerPrefs.Save();
            this.SetupUpgrade(weapon, upgrade);
        }

        // private void ToggleWeapon(Toggle toggle, bool isOn)
        // {
        //     Transform upgrade = toggle.transform.parent;
        //     string weaponName = upgrade.transform.Find("WeaponName").GetComponent<TextMeshProUGUI>().text.Split('+')[0].Trim();

        //     if (isOn)
        //     {
        //         if (StateManager.State.activeWeapons.Count >= 4)
        //         {
        //             toggle.isOn = false;
        //             return;
        //         }

        //         upgrade.GetComponent<Image>().color = Color.green;
                
        //         if (!StateManager.State.activeWeapons.Contains(weaponName))
        //         {
        //             StateManager.State.activeWeapons.Add(weaponName);
        //             // PlayerPrefs.SetString("ActiveWeapons", string.Join("|", this.activeWeapons));
        //             // PlayerPrefs.Save();
        //         }

        //         return;
        //     }

        //     upgrade.GetComponent<Image>().color = new Color(1, 170f/255f, 0);
        //     StateManager.State.activeWeapons.Remove(weaponName);
        //     // PlayerPrefs.SetString("ActiveWeapons", string.Join("|", this.activeWeapons));
        //     // PlayerPrefs.Save();
        // }
    }
}
