using Runtime.Enums;
using TMPro;
using UnityEngine;

namespace Runtime.Interfaces
{
    public interface IBuyable
    {

        BuyType GetBuyableType();

        void OnBuy(int amount);
        
     
        
         float GetPrice();

         


         void OnExitBuyArea();
    }
}