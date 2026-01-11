using System;
using System.Collections;
using DG.Tweening;
using Runtime.Enums;
using Runtime.Interfaces;
using Runtime.Keys;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class ResourceManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI gemText;

        #endregion

        #region Private Variables

        private ResourcesKeys _resourcesKeys;
        private Coroutine _buyCoroutine;
        private IBuyable _currentBuyable;
        private float _buyableResource;
        #endregion

        #endregion

        private void Awake()
        {
            _resourcesKeys = new ResourcesKeys(999,100);
        }

        private void Start()
        {
            GameSignals.Instance.onUpdateCoinText?.Invoke(_resourcesKeys.money);
            GameSignals.Instance.onUpdateGemText?.Invoke(_resourcesKeys.gem);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GameSignals.Instance.onUpdateCoinText += OnUpdateCoinText;
            GameSignals.Instance.onGetResourceKeys += OnGetResourceKeys;
            PlayerSignals.Instance.onPlayerEnteredBuyArea += OnPlayerEnteredBuyArea;
            PlayerSignals.Instance.onPlayerExitedBuyArea += OnPlayerExitedBuyArea;
        }

        private void OnPlayerExitedBuyArea()
        {
            if(_buyCoroutine != null)
                _currentBuyable.OnExitBuyArea();
            StopCoroutine(_buyCoroutine);
        }

        private void OnPlayerEnteredBuyArea(GameObject obj,Transform playerTransform)
        {
            _buyCoroutine = StartCoroutine(StartBuyingProcess(obj,playerTransform));
        }

        private IEnumerator StartBuyingProcess(GameObject buyable, Transform playerTransform)
        {
             _currentBuyable = buyable.GetComponent<IBuyable>();
            
             _buyableResource = _currentBuyable.GetBuyableType() == BuyType.Money ? _resourcesKeys.money : _resourcesKeys.gem;
            while (_buyableResource >= _currentBuyable.GetPrice() && _currentBuyable.GetPrice() >0)
            {
                yield return new WaitForSeconds(0.1f);
                
                _resourcesKeys.AddMoney(-1);
                _currentBuyable.OnBuy(1);

                var objType = _currentBuyable.GetBuyableType() == BuyType.Money ? PoolType.DecorativeMoney : PoolType.DecorativeGem;
                var resourceObj = PoolSignals.Instance.onGetPoolObject?.Invoke(objType);
                resourceObj.transform.parent = null;
                resourceObj.transform.position = playerTransform.position;
                   
                resourceObj.SetActive(true);
                resourceObj.transform.DOLocalJump(new Vector3(resourceObj.transform.position.x,2f,resourceObj.transform.position.z),1f,1,0.5f).OnComplete(() =>
                {
                    resourceObj.transform.DOMove(buyable.transform.position, 0.5f).OnComplete(() =>
                    {
                        PoolSignals.Instance.onSendPoolObject?.Invoke( resourceObj,PoolType.DecorativeMoney);
                    });
                });
               

            }

            yield return null;

        }

        private ResourcesKeys OnGetResourceKeys() => _resourcesKeys;
        

        private void OnUpdateCoinText(int value)
        {
            coinText.text = _resourcesKeys.money.ToString();
        }

        private void UnSubscribeEvents()
        {
            GameSignals.Instance.onUpdateGemText -= OnUpdateGemText;
            GameSignals.Instance.onUpdateCoinText -= OnUpdateCoinText; 
            PlayerSignals.Instance.onPlayerEnteredBuyArea -= OnPlayerEnteredBuyArea; 
            PlayerSignals.Instance.onPlayerExitedBuyArea -= OnPlayerExitedBuyArea;
        }

        private void OnUpdateGemText(int value)
        {
            gemText.text = _resourcesKeys.gem.ToString();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}