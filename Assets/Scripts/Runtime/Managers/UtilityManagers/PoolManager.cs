using System;
using System.Collections.Generic;
using Runtime.Data.UnityObjects;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.Rendering;

namespace Runtime.Managers
{
    public class PoolManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CD_PoolData poolData;

        #endregion

        #region Private Variables

        private List<GameObject> _pool = new List<GameObject>();
        #endregion

        #endregion

        private void Awake()
        {
            InitPool();
        }

        private void InitPool()
        {
            var poolSize = poolData.pools.Count;
            for (int i = 0; i < poolSize; i++)
            {
                var poolParent = new GameObject();
                poolParent.name = poolData.pools[i].poolType.ToString();
                poolParent.transform.SetParent(this.transform);
                for (int j = 0; j < poolData.pools[i].poolSize; j++)
                {
                    var poolObj = Instantiate(poolData.pools[i].poolPrefab, Vector3.zero,
                        Quaternion.identity, poolParent.transform);
                    poolObj.SetActive(false);


                }
            }
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PoolSignals.Instance.onGetPoolObject += OnGetPoolObject;
            PoolSignals.Instance.onSendPoolObject += OnSendPoolObject;
        }
        private GameObject OnGetPoolObject(PoolType poolType)
        {
            var poolParent = transform.GetChild((int)poolType);
            if (poolParent.childCount < 1 )
            {
                Debug.LogError("Pool is empty!");
                return InstantiateNewPoolObject(poolType);
            }
            var poolObj = poolParent.GetChild(poolParent.childCount - 1).gameObject;
            return poolObj; 
        }

        private GameObject InstantiateNewPoolObject(PoolType poolType)
        {
            var data = poolData.pools[(int)poolType];
            var poolObj = Instantiate(data.poolPrefab,Vector3.zero,Quaternion.identity,
                transform.GetChild((int)poolType));
            poolObj.SetActive(false);
            return poolObj;
        }

        private void OnSendPoolObject(GameObject poolObj, PoolType poolType)
        {
            poolObj.SetActive(false);
            var poolParent = transform.GetChild((int)poolType);
            poolObj.transform.SetParent(poolParent);
            
            
            
        }
        private void UnSubscribeEvents()
        {
            PoolSignals.Instance.onGetPoolObject -= OnGetPoolObject;
            PoolSignals.Instance.onSendPoolObject -= OnSendPoolObject;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

      
    }
}