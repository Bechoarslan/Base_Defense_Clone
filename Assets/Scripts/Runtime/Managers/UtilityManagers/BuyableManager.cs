
using Runtime.Enums;
using Runtime.Interfaces;
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
        public SpriteRenderer Renderer;
        public TextMeshPro TextMeshPro;
       
        #endregion
        #region Serialized Variables
        [SerializeField] private BuyableType buyableType;
        [SerializeField] private BuyType buyType;
        [SerializeField] private Transform defaultTransform;
        

        #endregion

        #region Private Variables

        private float _currentFill;
       
        
        #endregion

        #endregion

        private void Awake()
        {
            SetPriceText();
            _currentFill = 1 / Price;
            
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

        public BuyType GetBuyableType() => buyType;
       

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
                    bulletCarrier.transform.SetParent(defaultTransform);
                    bulletCarrier.transform.position = transform.position;
                    bulletCarrier.SetActive(true);
                    defaultTransform.gameObject.SetActive(true);
                    break;
                case BuyableType.NPCMoneyCollector:
                    var moneyCollector = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.NPCMoneyCollector);
                    moneyCollector.transform.position = transform.position;
                    moneyCollector.SetActive(true);
                    break;
                case BuyableType.NPCTurretController:
                    var turretController = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.NPCTurretController);
                    turretController.transform.SetParent(defaultTransform.transform);
                    turretController.transform.localPosition = Vector3.zero;
                    turretController.transform.localRotation = Quaternion.identity;
                    turretController.SetActive(true);
                    break;
                case BuyableType.LevelWall:
                    defaultTransform.gameObject.SetActive(false);
                    break;
                case BuyableType.LeftTurretWall:
                    gameObject.SetActive(false);
                    defaultTransform.transform.gameObject.SetActive(true);
                    break;
                case BuyableType.RightTurretWall:
                    gameObject.SetActive(false);
                    defaultTransform.transform.gameObject.SetActive(true);
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