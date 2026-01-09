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
        #endregion

        #endregion

        private void Awake()
        {
            _resourcesKeys = new ResourcesKeys(999,0);
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
            while (_resourcesKeys.money >= _currentBuyable.GetPrice() && _currentBuyable.GetPrice() >0)
            {
                yield return new WaitForSeconds(0.1f);
                
                _resourcesKeys.AddMoney(-1);
                _currentBuyable.OnBuy(1);

                var money = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.DecorativeMoney);
                money.transform.parent = null;
                money.transform.position = playerTransform.position;
                   
                money.SetActive(true);
                money.transform.DOLocalJump(new Vector3(money.transform.position.x,2f,money.transform.position.z),1f,1,0.5f).OnComplete(() =>
                {
                    money.transform.DOMove(buyable.transform.position, 0.5f).OnComplete(() =>
                    {
                        PoolSignals.Instance.onSendPoolObject?.Invoke( money,PoolType.DecorativeMoney);
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