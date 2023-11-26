using System;
using System.Collections.Generic;
using DG.Tweening;
using RunTime.Commands.StackManager;
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
        private StackAddBulletToPlayerCommand _stackAddBulletToPlayerCommand;
        [ShowInInspector] private List<GameObject> _bulletList = new List<GameObject>();
        private float stackOffset = 0;

        #endregion

        #endregion

        private void Awake()
        {
            _stackData = GetStackData();
            _poolData = GetPoolData();
            Init();
        }

        private void Init()
        {
            _stackAddBulletToPlayerCommand = new StackAddBulletToPlayerCommand(ref _stackData,transform,ref _bulletList);
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
            StackSignals.Instance.onPlayerInteractWithTurretBulletArea += OnPlayerInteractWithTurretBulletArea;
        }

        private void OnPlayerInteractWithTurretBulletArea(Transform turretArea)
        {
            var data = _stackData.Data[(int)PoolType.Bullet]._bulletData;
            for (int i =  _bulletList.Count; i > 0 ; i--)
            {
                if ( turretArea.childCount > 0 && turretArea.childCount % 4 <= 0)
                {
                    stackOffset += 0.500f;
                }
                
                
                var obj = _bulletList[^1];
                var index = turretArea.childCount % data.Count;
                obj.transform.parent = turretArea;
                var newPos = new Vector3(data[index].x, data[index].y + stackOffset , data[index].z);
                obj.transform.DOLocalMove(newPos, 1f);
                obj.transform.DORotate(Vector3.zero, 1f);
                _bulletList.Remove(obj);
                
            }
        }

        private void OnPlayerInteractWithBulletArea(Transform bulletArea)
        {
            _stackAddBulletToPlayerCommand.Execute(bulletArea);
            
        }
        
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            StackSignals.Instance.onPlayerInteractWithBulletArea -= OnPlayerInteractWithBulletArea;
            StackSignals.Instance.onPlayerInteractWithTurretBulletArea -= OnPlayerInteractWithTurretBulletArea;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        
        private void OnPlay()
        {
            
        }
        
    }
}