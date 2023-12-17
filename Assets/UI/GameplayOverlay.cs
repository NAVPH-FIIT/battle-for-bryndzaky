using System.Collections;
using System.Collections.Generic;
using Bryndzaky.Units.Player;
using Bryndzaky.Units.Laszlo;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using Bryndzaky.Combat;
using Bryndzaky.Combat.Weapons;
using Unity.VisualScripting;
using System.Linq;

namespace Bryndzaky.General.Common
{
    public partial class GameplayOverlay : MonoBehaviour
    {
        Slider healthBar;
        Slider xpBar;
        TextMeshProUGUI xpText;
        TextMeshProUGUI goldText;

        void Start()
        {
            this.activeWeaponImage = transform.Find("ActiveWeaponSlot").Find("ActiveWeapon").GetComponent<Image>();
            this.weaponText = transform.Find("WeaponWheel").GetComponentInChildren<TextMeshProUGUI>();
            this.weaponWheel = transform.Find("WeaponWheel").gameObject;
            this.healthBar = transform.Find("HealthBar").GetComponent<Slider>();
            this.xpBar = transform.Find("XPBar").GetComponent<Slider>();
            this.xpText = transform.Find("XPBar").Find("XPText").GetComponent<TextMeshProUGUI>();
            this.goldText = transform.Find("GoldCounter").GetComponentInChildren<TextMeshProUGUI>();

            // var activeWeapons = CombatManager.Instance.activeWeapons;
            // for (int i = 0; i < activeWeapons.Count; i++)
            //     weaponButtons[i].Initialize(activeWeapons[i].name, activeWeapons[i].weaponPrefab, activeWeapons[i].upgrade);
            this.UpdateWeaponWheel();
            // EventSystem.current.SetSelectedGameObject(button.gameObject);
            // var x = weaponButtons[0].buttonObject.GetComponent<EventTrigger>();
            // ExecuteEvents.Execute(weaponButtons[0].buttonObject, new PointerEventData(EventSystem.current), ExecuteEvents.selectHandler);
        }

        void Update()
        {
            this.healthBar.value = (((float)Player.Instance.GetHealth()) / Player.Instance.GetmaxHealth()) * 100;
            this.xpBar.value = (((float)StateManager.State.xp) / StateManager.State.NextLevel) * 100;
            this.xpText.text = StateManager.State.xp + " / " + StateManager.State.NextLevel;
            this.goldText.text = StateManager.State.gold.ToString();
        
            this.weaponWheel.SetActive(Input.GetButton("WeaponWheel"));
            this.UpdateWeaponWheel();
        }
    }

    public partial class GameplayOverlay
    {
        [SerializeField] private List<WeaponWheelButton> weaponButtons;
        private Image activeWeaponImage;
        private TextMeshProUGUI weaponText;
        private GameObject weaponWheel;
        private string previouslyActiveWeapons = "";

        [Serializable]
        private class WeaponWheelButton// : MonoBehaviour
        {
            // public int id;
            public GameObject buttonObject;
            public GameObject WeaponPrefab { get; private set; }
            public string WeaponName { get; private set; }
            public WeaponUpgrade WeaponUpgrade { get; private set; }
            public Sprite WeaponImage { get; private set; }
            public Animator Animator { get; private set; }
            [HideInInspector] public bool selected;// { get; set; }

            public void Initialize(CombatManager.ActiveWeapon activeWeapon)//string weaponName, GameObject prefab, WeaponUpgrade weaponUpgrade)
            {
                Button buttonComponent = this.buttonObject.GetComponent<Button>();
                Image imageComponent = this.buttonObject.transform.Find("Image").GetComponent<Image>();

                if (activeWeapon == null)
                {
                    buttonComponent.interactable = false;
                    imageComponent.enabled = false;
                    this.WeaponName = "";
                    this.Animator = null;
                    return;
                }

                this.WeaponName = activeWeapon.name;
                this.WeaponPrefab = activeWeapon.weaponPrefab;
                this.WeaponUpgrade = activeWeapon.upgrade;
                this.WeaponImage = activeWeapon.weaponPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
                this.Animator = this.buttonObject.GetComponent<Animator>();

                buttonComponent.interactable = true;
                imageComponent.enabled = true;
                imageComponent.sprite = this.WeaponImage;
            }
        }

        public void UpdateWeaponWheel()
        {
            var activeWeapons = CombatManager.Instance.ActiveWeapons;
            string newActiveWeapons = string.Join('|', activeWeapons.Select(w => w.upgrade.name));
            // Debug.LogError("New: " + newActiveWeapons + ", Previous: " + this.previouslyActiveWeapons);
            if (this.previouslyActiveWeapons == newActiveWeapons)
                return;

            this.activeWeaponImage.sprite = null;
            this.weaponText.text = "";
            this.previouslyActiveWeapons = newActiveWeapons;
            for (int i = 0; i < weaponButtons.Count; i++)
                weaponButtons[i].Initialize(i < activeWeapons.Count ? activeWeapons[i] : null);//activeWeapons[i].name, activeWeapons[i].weaponPrefab, activeWeapons[i].upgrade); 
            
            this.Select("0|true");
            // ExecuteEvents.Execute(weaponButtons[0].buttonObject, new PointerEventData(EventSystem.current), ExecuteEvents.selectHandler);
            // for (int i = 0; i < activeWeapons.Count; i++)
            //     weaponButtons[i].Initialize(activeWeapons[i].name, activeWeapons[i].weaponPrefab, activeWeapons[i].upgrade);
        }

        public void Select(string parameters)
        {
            //Debug.LogError("selected");
            int index = int.Parse(parameters.Split("|")[0]);
            bool selected = bool.Parse(parameters.Split("|")[1]);

            this.weaponButtons[index].selected = selected;
            if (selected)
            {
                Player.Instance.GrantWeapon(this.weaponButtons[index].WeaponPrefab, this.weaponButtons[index].WeaponUpgrade);
                this.activeWeaponImage.sprite = this.weaponButtons[index].WeaponImage;
            }
        }

        public void Hover(string parameters)
        {
            //Debug.LogError("hover");
            int index = int.Parse(parameters.Split("|")[0]);
            bool hover = bool.Parse(parameters.Split("|")[1]);

            this.weaponText.text = hover ? this.weaponButtons[index].WeaponName : "";
            this.weaponButtons[index].Animator?.SetBool("Hover", hover);
        }

    }
}
