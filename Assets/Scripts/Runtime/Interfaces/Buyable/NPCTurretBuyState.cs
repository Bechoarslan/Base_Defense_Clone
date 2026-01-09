using Runtime.Managers;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Interfaces.Buyable
{
    public class NPCTurretBuyState : IBuyable
    {
        private BuyableManager Manager;
        private float Price;
        private float currentFill;
        
        public NPCTurretBuyState(BuyableManager buyableManager)
        {
            Manager = buyableManager;
            Price = Manager.Price;
            SetPriceText();
            currentFill = 1 / Price;
        }

        

        public void OnBuy(int amount)
        {
            
            
        }

        public void SetPriceText()
        {
            
        }

        public void OnReadyToBuy()
        {
           
        }

        public float GetPrice()
        {
            return Price;
        }

        public void CheckThePriceLabel(int price)
        {
            
        }

        public void OnExitBuyArea()
        {
            Manager.Renderer.material.SetFloat("_Fill", 0f);
            Manager.Price = Price;
            currentFill = 1 / Price;
            SetPriceText();
        }

        public void CheckThePriceLabel()
        {
            
        }
    }
}