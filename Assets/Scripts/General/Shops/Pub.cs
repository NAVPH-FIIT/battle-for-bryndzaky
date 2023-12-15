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
    public class Pub : MonoBehaviour
    {
        [SerializeField] private GameObject consumablesContainer;
        private List<Button> purchaseButtons = new();
        private List<TextMeshProUGUI> priceTexts = new();
  

        public void Start()
        {            
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

        private void PurchaseConsumable(string name, int price)
        {
            var consumable = StateManager.State.consumables.Find(c => c.name == name);
            
            if (consumable != null)
                consumable.count++;
            else
                StateManager.State.consumables.Add(new StateManager.GameState.Consumable{name=name, count=1});
           
            StateManager.State.gold -= price;
        }
    }
}
