using System;
using DG.Tweening;
using RunTime.Commands.PlayerMovement;
using RunTime.Data.ValueObject;
using RunTime.Enums;
using RunTime.Keys;
using RunTime.Signals;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        [ShowInInspector] private Rigidbody _rigidbody;
        private PlayerMovementData _playerMovementData;
        private HorizontalInputParams _inputParams;
        [ShowInInspector]private bool _isReadyToMove, _isReadyToPlay,_isTurretPlay;
        

        private PlayerJoystickMovementCommand _playerJoystickMovementCommand;
        private PlayerTurretMovementCommand _playerTurretMovementCommand;
        private PlayerLookAtTurretCommand _playerLookAtTurretCommand;
        private GameObject _emptyObject;
        private Transform _playerTransform;
        #endregion

        private void Awake()
        {
            GetReferences();
            Init();
        }
        
        private void GetReferences()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        private void Init()
        {
            _playerJoystickMovementCommand = new PlayerJoystickMovementCommand();
            _playerTurretMovementCommand = new PlayerTurretMovementCommand();
            _playerLookAtTurretCommand = new PlayerLookAtTurretCommand();
        }

        #endregion

        public void UpdateInputValue(HorizontalInputParams inputParams) => _inputParams = inputParams;

        public void GetMovementDataFromManager(PlayerMovementData playerMovementData)
        {
            _playerMovementData = playerMovementData;
        } 

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onMoveConditionChanged += OnMoveConditionChanged;
            PlayerSignals.Instance.onPlayConditionChanged += OnPlayConditionChanged;
        }

        private void OnPlayConditionChanged(bool condition) => _isReadyToPlay = condition;
        

        private void OnMoveConditionChanged(bool condition) => _isReadyToMove = condition;
        

        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onMoveConditionChanged -= OnMoveConditionChanged;
            PlayerSignals.Instance.onPlayConditionChanged -= OnPlayConditionChanged;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void FixedUpdate()
        {
            
            if (_isReadyToPlay)
            {
                _playerJoystickMovementCommand.Execute(ref _playerMovementData, ref _inputParams, ref _rigidbody);
            }

            if (_isTurretPlay)
            {
                _playerTurretMovementCommand.Execute(ref _playerMovementData, ref _inputParams, ref _rigidbody,ref _emptyObject);
            }
            
        }

        internal void OnPlayerInteractWithTurret(GameObject turretObj)
        {
            _isTurretPlay = true;
            var playerManagerTransform = _rigidbody.transform;
            Transform turretTransform = turretObj.transform;
            _playerLookAtTurretCommand.Execute(ref playerManagerTransform, ref turretTransform);
            _emptyObject = turretObj;
            
            
            
            
        }

        internal void OnPlayerExitInteractWithTurret()
        {
            _isTurretPlay = false;
        } 
        
    }
}