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
        [ShowInInspector] private List<GameObject> _moneyList = new List<GameObject>();


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
            _stackAddBulletToPlayerCommand = new StackAddBulletToPlayerCommand(ref _bulletList,ref _stackData );
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
            _stackAddBulletToPlayerCommand.Execute(bulletArea, playerManager);
            
        }

        private void OnPlay()
        {
            
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

        private void Update()
        {
            
        }
    }
}