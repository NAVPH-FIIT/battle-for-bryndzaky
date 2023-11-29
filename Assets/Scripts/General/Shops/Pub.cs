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
    public class Pub : MonoBehaviour
    {
        [SerializeField] private GameObject consumablesContainer;
        private List<Button> purchaseButtons = new();
        private List<TextMeshProUGUI> priceTexts = new();
  

        public void Start()
        {
            //gameObject.SetActive(false);
            PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold", 100));
            PlayerPrefs.SetInt("gold", 1000); // TODO: Remove

            
            foreach (Transform consumable in this.consumablesContainer.transform)
            {
                string name = consumable.Find("ConsumableName").GetComponent<TextMeshProUGUI>().text;
                
                var purchaseUI = consumable.Find("PurchaseUI");
                TextMeshProUGUI pricetext = purchaseUI.Find("PriceText").GetComponent<TextMeshProUGUI>();
                Button purchaseButton = purchaseUI.Find("PurchaseButton").GetComponent<Button>();
                
                this.priceTexts.Add(pricetext);
                this.purchaseButtons.Add(purchaseButton);

                purchaseButton.onClick.AddListener(() => this.PurchaseConsumable(name, int.Parse(pricetext.text.Split(' ')[0])));
            }
        }

        public void Update()
        {
            int playerGold = PlayerPrefs.GetInt("gold", 0);
            for (int i = 0; i < this.purchaseButtons.Count; i++)
                if (int.Parse(this.priceTexts[i].text.Split(' ')[0]) > playerGold)
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

        private void PurchaseConsumable(string name, int price)
        {
            PlayerPrefs.SetInt(name, PlayerPrefs.GetInt(name, 0) + 1);
            PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") - price);
            PlayerPrefs.Save();
        }
    }
}
