using TMPro;
using UnityEngine;

namespace Runtime.Interfaces
{
    public interface IBuyable
    {


        void OnBuy(int amount);
        
        void SetPriceText();

        void OnReadyToBuy();
        
         float GetPrice();

         void CheckThePriceLabel(int price);


         void OnExitBuyArea();
    }
}