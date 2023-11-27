using System;
using System.Collections.Generic;
using DG.Tweening;
using RunTime.Commands.StackManager;
using RunTime.Data.UnityObject;
using RunTime.Data.ValueObject;
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
        
        private StackAddBulletToPlayerCommand _stackAddBulletToPlayerCommand;
        [ShowInInspector] private List<GameObject> _bulletList = new List<GameObject>();
        private PlayerData _stackData;

        #endregion

        #endregion

        private void Awake()
        {
            _stackData = GetStackData();
            Init();
        }

        private PlayerData GetStackData() => Resources.Load<CD_PlayerData>("Data/CD_PlayerData").data;
        

        private void Init()
        {
            _stackAddBulletToPlayerCommand = new StackAddBulletToPlayerCommand(ref _stackData,transform,ref _bulletList);
        }
        
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            PlayerSignals.Instance.onPlayerInteractWithBulletArea += OnPlayerInteractWithBulletArea;
            PlayerSignals.Instance.onPlayerInteractWithTurretBulletArea += OnPlayerInteractWithTurretBulletArea;
        }

        private void OnPlayerInteractWithTurretBulletArea(Transform turretArea)
        {
            var data = _stackData.StackData[(int)PoolType.Bullet].BulletData;
            
            for (int i =  _bulletList.Count; i > 0 ; i--)
            {
                var areaChildCount = turretArea.childCount;
                var stackValue = areaChildCount / 4;
                var obj = _bulletList[^1];
                var index = areaChildCount % data.Count;
                
                
                obj.transform.parent = turretArea;
                var newPos = new Vector3(data[index].x, data[index].y + stackValue * 0.500f, data[index].z);
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
            PlayerSignals.Instance.onPlayerInteractWithBulletArea -= OnPlayerInteractWithBulletArea;
            PlayerSignals.Instance.onPlayerInteractWithTurretBulletArea -= OnPlayerInteractWithTurretBulletArea;
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