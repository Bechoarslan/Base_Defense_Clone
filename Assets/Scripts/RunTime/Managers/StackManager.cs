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
using Random = UnityEngine.Random;


namespace RunTime.Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Private Variables
        
        private StackAddBulletToPlayerCommand _stackAddBulletToPlayerCommand;
        private StackAddBulletToBulletAreaCommand _stackAddBulletToBulletAreaCommand;
        [ShowInInspector] private List<GameObject> _bulletList = new List<GameObject>();
        private PlayerData _stackData;
        private Transform _bulletArea;
        private bool _isGotStack;

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
            _stackAddBulletToBulletAreaCommand = new StackAddBulletToBulletAreaCommand(ref _stackData, ref _bulletList);
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
            PlayerSignals.Instance.onPlayerInteractEnterArea += OnPlayerInteractEnterArea;
        }

        private void OnPlayerInteractEnterArea()
        {
            if(_bulletList.Count <= 0 ) return;
         
            for (var i = _bulletList.Count; i > 0; i--)
            {
                var obj = _bulletList[i - 1];
                _bulletList.Remove(obj);
                obj.transform.parent = _bulletArea;
                var randomXPos = Random.Range(-0.5f, 0.5f);
                var randomRot = Random.Range(-180, 180);
                var newPos = new Vector3(obj.transform.position.x + randomXPos, obj.transform.position.y, obj.transform.position.z + randomXPos);
                var newRotation = new Vector3(0, 0, randomRot);
                obj.transform.DOLocalRotate(newRotation, 2f);
                obj.transform.DOMove(newPos, 0.5f).OnComplete(() =>
                {
                    obj.transform.DOMove(_bulletArea.position, 0.5f).OnComplete(() =>
                    {
                        obj.SetActive(false);
                        PoolSignals.Instance.onSendPool?.Invoke(obj,PoolType.Bullet);
                    });
                    
                });
               
                
                


            }
            
        }

        private void OnPlayerInteractWithTurretBulletArea(Transform turretArea)
        {
            
            _stackAddBulletToBulletAreaCommand.Execute(ref turretArea);
            _isGotStack = false;
        }
            
        
        private void OnPlayerInteractWithBulletArea(Transform bulletArea) 
        {
            _bulletArea = bulletArea;
            _stackAddBulletToPlayerCommand.Execute(ref bulletArea, ref _isGotStack);
            _isGotStack = true;
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