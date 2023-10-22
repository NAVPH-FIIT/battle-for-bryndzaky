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

namespace Bryndzaky.General.Common
{
    public partial class GameplayOverlay : MonoBehaviour
    {
        Slider healthBar;

        void Start()
        {
            this.activeWeaponImage = transform.Find("ActiveWeaponSlot").Find("ActiveWeapon").GetComponent<Image>();
            this.weaponText = GetComponentInChildren<TextMeshProUGUI>();
            this.weaponWheel = transform.Find("WeaponWheel").gameObject;
            this.healthBar = transform.Find("HealthBar").GetComponent<Slider>();

            var activeWeapons = CombatManager.Instance.activeWeapons;
            for (int i = 0; i < weaponButtons.Count; i++)
                weaponButtons[i].Initialize(activeWeapons[i].name, activeWeapons[i].prefab);

            // EventSystem.current.SetSelectedGameObject(button.gameObject);
            // var x = weaponButtons[0].buttonObject.GetComponent<EventTrigger>();
            ExecuteEvents.Execute(weaponButtons[0].buttonObject, new PointerEventData(EventSystem.current), ExecuteEvents.selectHandler);
        }

        void Update()
        {
            healthBar.value = Player.Instance.GetHealth();
            this.weaponWheel.SetActive(Input.GetKey(KeyCode.LeftControl));
        }
    }

    public partial class GameplayOverlay
    {
        [SerializeField] private List<WeaponWheelButton> weaponButtons;
        private Image activeWeaponImage;
        private TextMeshProUGUI weaponText;
        private GameObject weaponWheel;

        [Serializable]
        private class WeaponWheelButton// : MonoBehaviour
        {
            // public int id;
            public GameObject buttonObject;
            public GameObject WeaponPrefab { get; private set; }
            public string WeaponName { get; private set; }
            public Sprite WeaponImage { get; private set; }
            public Animator Animator { get; private set; }
            [HideInInspector] public bool selected;// { get; set; }

            public void Initialize(string weaponName, GameObject prefab)
            {
                this.WeaponName = weaponName.Replace('_', ' ');
                this.WeaponPrefab = prefab;
                this.WeaponImage = prefab.GetComponentInChildren<SpriteRenderer>().sprite;
                this.Animator = this.buttonObject.GetComponent<Animator>();

                this.buttonObject.GetComponent<Button>().interactable = true;
                // Debug.LogError(this.buttonObject.GetComponentInChildren<Image>().name);
                this.buttonObject.transform.Find("Image").GetComponent<Image>().sprite = this.WeaponImage;
            }
        }

        public void Select(string parameters)
        {
            Debug.LogError("selected");
            int index = int.Parse(parameters.Split("|")[0]);
            bool selected = bool.Parse(parameters.Split("|")[1]);

            this.weaponButtons[index].selected = selected;
            if (selected)
            {
                Player.Instance.GrantWeapon(this.weaponButtons[index].WeaponPrefab);
                this.activeWeaponImage.sprite = this.weaponButtons[index].WeaponImage;
            }
        }

        public void Hover(string parameters)
        {
            // Debug.LogError("hover");
            int index = int.Parse(parameters.Split("|")[0]);
            bool hover = bool.Parse(parameters.Split("|")[1]);

            this.weaponText.text = hover ? this.weaponButtons[index].WeaponName : "";
            this.weaponButtons[index].Animator.SetBool("Hover", hover);
        }
    }
}