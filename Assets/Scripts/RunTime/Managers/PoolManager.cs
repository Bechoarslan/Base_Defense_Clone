using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RunTime.Commands.Pool;
using RunTime.Data.UnityObject;
using RunTime.Enums.Pool;
using RunTime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RunTime.Managers
{
    public class PoolManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject levelHolder;
        [SerializeField] private Transform poolHolder;

        #endregion

        #region Private Variables

        private CD_PoolData _poolData;
        private GameObject _emptyObject;
        private PoolGenerateCommand _poolGenerateCommand;
        private PoolResetCommand _poolResetCommand;
        private List<GameObject> _emptyList = new List<GameObject>();
        private readonly string _poolDataPath = "Data/CD_PoolData";

        #endregion

        #endregion

        private void Awake()
        {
            _poolData = GetPoolData();
            Init();
            GeneratePool();
        }

        private void GeneratePool()
        {
            _poolGenerateCommand.Execute();
        }

        private void Init()
        {
            _poolGenerateCommand = new PoolGenerateCommand(ref _poolData, ref poolHolder, ref _emptyObject);
            _poolResetCommand = new PoolResetCommand(ref _poolData, ref poolHolder, ref levelHolder);
        }

        private CD_PoolData GetPoolData() => Resources.Load<CD_PoolData>(_poolDataPath);

        private async void RestartPool()
        {
            await Task.Delay(500);
            _poolResetCommand.Execute();
        }
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += RestartPool;
            PoolSignals.Instance.onSendPool += OnSendPool;
            PoolSignals.Instance.onGetPoolObject += OnGetPoolObject;
        }
        

        private GameObject OnGetPoolObject(PoolType poolType)
        {
            
            var parent = poolHolder.transform.GetChild((int)poolType);
            
            if (parent.childCount > 0)
            {
                var obj = parent.transform.GetChild(0).gameObject;
                return obj;
            }
            else
            {
                var obj = Instantiate(_poolData.Data[(int)poolType].Prefab,Vector3.zero,Quaternion.identity,poolHolder.GetChild((int)poolType));
                return obj;
            }
        }
        
        private void OnSendPool(GameObject poolObject, PoolType poolType)
        {
            poolObject.transform.parent = poolHolder.GetChild((int)poolType);
            poolObject.transform.localPosition = Vector3.zero;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= RestartPool;
            PoolSignals.Instance.onSendPool -= OnSendPool;
            PoolSignals.Instance.onGetPoolObject -= OnGetPoolObject;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}