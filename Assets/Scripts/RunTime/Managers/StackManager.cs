using System;
using System.Collections.Generic;
using DG.Tweening;
using RunTime.Data.UnityObject;
using RunTime.Enums.Pool;
using RunTime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RunTime.Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Private Variables

        private CD_StackData _stackData;
        private CD_PoolData _poolData;
        private readonly string _stackDataPath = "Data/CD_StackData";
     
        [ShowInInspector] private List<GameObject> _bulletList = new List<GameObject>();
        [ShowInInspector] private List<GameObject> _moneyList = new List<GameObject>();


        #endregion

        #endregion

        private void Awake()
        {
            _stackData = GetStackData();
            _poolData = GetPoolData();
        }

        private CD_PoolData GetPoolData() => Resources.Load<CD_PoolData>("Data/CD_PoolData");

        private CD_StackData GetStackData() => Resources.Load<CD_StackData>(_stackDataPath);

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            StackSignals.Instance.onPlayerInteractWithBulletArea += OnPlayerInteractWithBulletArea;
        }

        private void OnPlayerInteractWithBulletArea(Transform bulletArea, Transform playerManager)
        {
            

            for (int i = 0; i < _stackData.Data[(int)PoolType.Bullet].StackLimit; i++)
            {
                
                if (i == 0)
                {
                    _bulletList[i].transform.position = bulletArea.position;
                    _bulletList[i].gameObject.SetActive(true);
                    _bulletList[i].transform.parent = playerManager;
                    _bulletList[i].transform.position = playerManager.transform.position;
                    continue;
                }
                Debug.LogWarning(i);
                _bulletList[i].transform.position = bulletArea.position;
                _bulletList[i].gameObject.SetActive(true);
                var newPos = new Vector3(_bulletList[i-1].transform.position.x,
                    _bulletList[i-1].transform.position.y + 
                    0.30f,_bulletList[i-1].transform.position.z);
                _bulletList[i].transform.parent = playerManager;
                _bulletList[i].transform.position = newPos;

            }
        }

        private void OnPlay()
        {
            for (int i = 0; i < _poolData.Data[(int)PoolType.Bullet].ObjectCount ; i++)
            {
              var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Bullet);
              obj.transform.parent = transform;
              _bulletList.Add(obj);
            }
           
           
        }

        

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            StackSignals.Instance.onPlayerInteractWithBulletArea -= OnPlayerInteractWithBulletArea;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}