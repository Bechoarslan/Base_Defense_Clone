using TMPro;
using UnityEngine;

namespace Runtime.Interfaces.Buyable
{
    public class NPCMoneyCollectorBuyState : IBuyable
    {
        public float Price { get; set; }

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
            
        }

        public void CheckThePriceLabel()
        {
            
        }
    }
}