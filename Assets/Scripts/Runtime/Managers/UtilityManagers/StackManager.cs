using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Commands.Stack;
using Runtime.Data.UnityObjects;
using Runtime.Enums;
using Runtime.Keys;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CD_StackData ammoData;
        [SerializeField] private CD_StackData moneyData;

        [SerializeField] private Transform ammoStackHolder;

        #endregion

        #region Private Variables
        
        private StackSendObjectToHolderCommand _stackSendObjectToHolderCommand;
        private StackSendObjectToArea _stackSendObjectToArea;
        private CD_StackData _emptyStackData;
        private List<GameObject> _droppedMoneyList = new List<GameObject>();
        #endregion

        #endregion


        private void Awake()
        {
            _stackSendObjectToHolderCommand = new StackSendObjectToHolderCommand();
            _stackSendObjectToArea = new StackSendObjectToArea();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onSendStacksToHolder += OnSendPlayerStacksToHolder;
            GameSignals.Instance.onSendBulletStackObjectToHolder += OnSendStackObjectToHolder;
            GameSignals.Instance.onSendBulletStackObjectToArea += OnSendstackObjectToArea;
            GameSignals.Instance.onGetStackAmmoHolderTransform += OnGetAmmoStackHolderTransform;
            GameSignals.Instance.onSendMoneyStackToHolder += OnSendMoneyStackToHolder;
            GameSignals.Instance.onAddListDroppedMoneyFromEnemy += OnAddListDroppedMoneyFromEnemy;
            GameSignals.Instance.onGetMoneyStackList += OnGetMoneyStackList;
        }

        private List<GameObject> OnGetMoneyStackList() => _droppedMoneyList;

        private void OnAddListDroppedMoneyFromEnemy(GameObject obj)
        {
            if (_droppedMoneyList.Contains(obj)) return;
            _droppedMoneyList.Add(obj);
        }
       

        private void OnSendMoneyStackToHolder(Transform stackHolder, GameObject stackObj,float stackCountLimit)
        {
            if(stackHolder.childCount >= stackCountLimit)
                return;
            if(_droppedMoneyList.Contains(stackObj)) 
                _droppedMoneyList.Remove(stackObj);
            Debug.Log("Money Stack Sent To Holder");
            stackObj.tag = "Untagged";
            var stackHolderCount = stackHolder.childCount;
            stackObj.transform.SetParent(stackHolder);
            stackObj.transform.DOLocalJump(
                new Vector3(0, 2f, 0),
                2f, 1, 0.3f).OnComplete(() =>
            {
               var newPosition = new Vector3(0, moneyData.stackData.StackHolderOffset.y * stackHolderCount, moneyData.stackData.StackHolderOffset.z);
                stackObj.transform.localRotation = Quaternion.Euler(-90,0, -90);
                 stackObj.transform.DOLocalMove(newPosition, 0.5f);
            });
        }

        private Transform OnGetAmmoStackHolderTransform() => ammoStackHolder;
       

        private void OnSendPlayerStacksToHolder(Transform playerStackHolder,PoolType poolType)
        {
            var tag = "Untagged";
            if (poolType == PoolType.Money)
            {
                tag = "Money";
            }
            var count = playerStackHolder.childCount;
            Debug.Log(count);
            for (int i = count - 1; i >= 0; i--)
            {
                var stackObj = playerStackHolder.GetChild(i).gameObject;

                if (poolType == PoolType.Money)
                {
                    GameSignals.Instance.onGetResourceKeys?.Invoke().AddMoney(5);
                }
                stackObj.transform.DOLocalJump(new Vector3(0, 2f, 0), 1f, 1, 0.5f).OnComplete(() =>
                {
                    stackObj.transform.SetParent(ammoStackHolder);
                    stackObj.transform.DOMove(ammoStackHolder.position, 0.5f).OnComplete(() =>
                    {
                        stackObj.tag = tag;
                        PoolSignals.Instance.onSendPoolObject?.Invoke(stackObj,poolType);
                    });

                });
                
            }
        }

        [Button]
        private void TrySomething()
        {
            GameSignals.Instance.onGetResourceKeys?.Invoke().AddMoney(1);
        }

        private IEnumerator OnSendstackObjectToArea(Transform areaHolder, Transform stackHolder, StackType stackType)
        {
            var waiter = new WaitForSeconds(0.3f);
            while (stackHolder.childCount > 0)
            {
                var obj = stackHolder.GetChild(stackHolder.childCount - 1 ).gameObject;
                SetStackObjectToArea(obj, areaHolder, stackHolder, stackType);
                yield return waiter;
            }
        }

        private void SetStackObjectToArea(GameObject stackObj, Transform areaHolder, Transform stackHolder, StackType stackType)
        {
            switch (stackType)
            {
                case StackType.Ammo:
                    _stackSendObjectToArea.Execute(stackObj, areaHolder, stackHolder, ammoData);
                    break;
            }
        }

        private IEnumerator OnSendStackObjectToHolder(Transform areaHolder, Transform stackHolder, StackType stackType,float stackLimit)
        {
        
            _emptyStackData = stackType switch
            {
                StackType.Ammo => ammoData,
                StackType.Money => moneyData,
                _ => _emptyStackData
            };
            
            var waiter = new WaitForSeconds(0.2f);
            while (stackHolder.childCount < stackLimit)
            {
                var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Ammo);
                if (obj is null) break;
                SetObjectPosition(obj,areaHolder, stackHolder, stackType);
                Debug.Log("Stack Object Sending To Holder");
                yield return waiter;
            }
           

        }
        private void SetObjectPosition(GameObject stackObj,Transform areaHolder, Transform stackHolder, StackType stackType)
        {
            switch (stackType)
            {
                case StackType.Ammo:
                    _stackSendObjectToHolderCommand.Execute(stackObj, areaHolder, stackHolder, ammoData);
                    break;
                case StackType.Money:
                    
                    break;
            }
        }


        private void UnSubscribeEvents()
        {
            GameSignals.Instance.onSendBulletStackObjectToHolder -= OnSendStackObjectToHolder;
            GameSignals.Instance.onSendBulletStackObjectToArea -= OnSendstackObjectToArea;
            PlayerSignals.Instance.onSendStacksToHolder -= OnSendPlayerStacksToHolder; 
            GameSignals.Instance.onGetStackAmmoHolderTransform -= OnGetAmmoStackHolderTransform;
            GameSignals.Instance.onSendMoneyStackToHolder -= OnSendMoneyStackToHolder;
            GameSignals.Instance.onAddListDroppedMoneyFromEnemy -= OnAddListDroppedMoneyFromEnemy;
            GameSignals.Instance.onGetMoneyStackList -= OnGetMoneyStackList;
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}