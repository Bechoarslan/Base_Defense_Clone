using System;
using Runtime.Enums;
using Runtime.Interfaces;
using Runtime.Interfaces.Buyable;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class BuyableManager : MonoBehaviour,IBuyable
    {
        #region Self Variables

        #region Public Variables

        public float Price = 100;
        public IBuyable Buyable;
       
        public SpriteRenderer Renderer;
        public TextMeshPro TextMeshPro;
       
        #endregion
        #region Serialized Variables
        [SerializeField] private BuyableType buyableType;
        [SerializeField] private BuyType buyType;

        #endregion

        #region Private Variables

        private float _currentFill;
       
        
        #endregion

        #endregion

        private void Awake()
        {
            InitBuyable();
            SetPriceText();
            _currentFill = 1 / Price;
            
        }

        private void InitBuyable()
        {
            switch (buyableType)
            {
                case BuyableType.NPCTurretController:
                    Buyable = new NPCTurretBuyState(this);
                    break;
                case BuyableType.NPCAmmoCarrier:
                    Buyable = new NPCAmmoCarrierBuyState();
                    break;
                case BuyableType.NPCMoneyCollector:
                    Buyable = new NPCMoneyCollectorBuyState();
                    break;
            }
       
            
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GameSignals.Instance.onUpdateCoinText += CheckThePriceLabel; 
            GameSignals.Instance.onUpdateGemText += CheckThePriceLabel;
        }

       

        private void UnSubscribeEvents()
        {
            GameSignals.Instance.onUpdateCoinText -= CheckThePriceLabel;
            GameSignals.Instance.onUpdateGemText -= CheckThePriceLabel;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        public void OnBuy(int amount)
        {
            var fill = Renderer.material.GetFloat("_Fill");
            fill += _currentFill;
            Price -= amount;
            Renderer.material.SetFloat("_Fill", fill);
            if (Price <= 0)
            {
                OnReadyToBuy();
            }
            SetPriceText();
        }

        public void SetPriceText()
        {
            TextMeshPro.text = Price.ToString();
        }

        public void OnReadyToBuy()
        {
           PlayerSignals.Instance.onPlayerExitedBuyArea?.Invoke();
           Price = 100;
           CheckThePriceLabel(0);
           Renderer.material.SetFloat("_Fill", 0);
           CreateBoughtItem();
        }

        private void CreateBoughtItem()
        {
            switch (buyableType)
            {
                case BuyableType.NPCAmmoCarrier:
                    var bulletCarrier = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.NPCBulletCarrier);
                    bulletCarrier.transform.position = transform.position;
                    bulletCarrier.SetActive(true);
                    break;
            }
        }

        public float GetPrice()
        {
            return Price;
        }
        public void OnExitBuyArea()
        {
           Renderer.material.SetFloat("_Fill", 0f);
            _currentFill = 1 / Price;
            SetPriceText();
        }
        
        public void CheckThePriceLabel(int price)
        {
            var resource = GameSignals.Instance.onGetResourceKeys();
            switch (buyType)
            {
                case BuyType.Gem:

                    CheckIt(resource.gem);
                    break;
                case BuyType.Money:
                    CheckIt(resource.money);
                    break;
            }
            
           
        }

        private void CheckIt(int budget)
        {
            if (budget >= Price)
            {
                TextMeshPro.color = Color.green;
            }
            else
            {
                TextMeshPro.color = Color.red;
            }
        }
    }
}