using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Commands.Stack;
using Runtime.Data.UnityObjects;
using Runtime.Enums;
using Runtime.Signals;
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
            StackSignals.Instance.onSendStackObjectToHolder += OnSendStackObjectToHolder;
            StackSignals.Instance.onSendStackObjectToArea += OnSendstackObjectToArea;
            StackSignals.Instance.onGetAmmoStackHolderTransform += OnGetAmmoStackHolderTransform;
        }

        private Transform OnGetAmmoStackHolderTransform() => ammoStackHolder;
       

        private void OnSendPlayerStacksToHolder(Transform playerStackHolder)
        {
            var count = playerStackHolder.childCount;
            Debug.Log(count);
            for (int i = count - 1; i > 0; i--)
            {
                var stackObj = playerStackHolder.GetChild(i).gameObject;
                stackObj.transform.DOLocalJump(new Vector3(0, 2f, 0), 1f, 1, 0.5f).OnComplete(() =>
                {
                    stackObj.transform.SetParent(ammoStackHolder);
                    stackObj.transform.DOMove(ammoStackHolder.position, 0.5f).OnComplete(() =>
                    {
                        PoolSignals.Instance.onSendPoolObject?.Invoke(stackObj,PoolType.Ammo);
                    });

                });
                
            }
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

        private IEnumerator OnSendStackObjectToHolder(Transform areaHolder, Transform stackHolder, StackType stackType)
        {
            _emptyStackData = stackType switch
            {
                StackType.Ammo => ammoData,
                StackType.Money => moneyData,
                _ => _emptyStackData
            };
            Debug.Log("here");
            var waiter = new WaitForSeconds(0.2f);
            while (stackHolder.childCount < _emptyStackData.stackData.StackLimit)
            {
                var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Ammo);
                if (obj is null) break;
                SetObjectPosition(obj,areaHolder, stackHolder, stackType);
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
            }
        }


        private void UnSubscribeEvents()
        {
            StackSignals.Instance.onSendStackObjectToHolder -= OnSendStackObjectToHolder;
            StackSignals.Instance.onSendStackObjectToArea -= OnSendstackObjectToArea;
            PlayerSignals.Instance.onSendStacksToHolder -= OnSendPlayerStacksToHolder;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}