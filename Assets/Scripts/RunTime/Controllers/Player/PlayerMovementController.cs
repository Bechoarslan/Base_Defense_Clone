using System;
using System.Collections;
using DG.Tweening;
using RunTime.Commands.PlayerMovement;
using RunTime.Data.ValueObject;
using RunTime.Enums;
using RunTime.Enums.Camera;
using RunTime.Keys;
using RunTime.Signals;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform itemHolder;

        #endregion

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
        private bool _isCollected;
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
            StackSignals.Instance.onPlayerInteractWithBulletArea += OnPlayerInteractWithBulletArea;
        }

        private void OnPlayConditionChanged(bool condition) => _isReadyToPlay = condition;
        

        private void OnMoveConditionChanged(bool condition) => _isReadyToMove = condition;
        

        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onMoveConditionChanged -= OnMoveConditionChanged;
            PlayerSignals.Instance.onPlayConditionChanged -= OnPlayConditionChanged;
            StackSignals.Instance.onPlayerInteractWithBulletArea -= OnPlayerInteractWithBulletArea;
        }

        private void OnPlayerInteractWithBulletArea(Transform arg0, Transform arg1)
        {
            _isCollected = true;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void FixedUpdate()
        {
            
            if (_isReadyToPlay)
            {
                _playerJoystickMovementCommand.Execute(ref _playerMovementData, ref _inputParams, ref _rigidbody, itemHolder, ref _isCollected);
            }

            if (_isTurretPlay)
            {
                _playerTurretMovementCommand.Execute(ref _playerMovementData, ref _inputParams, ref _rigidbody,ref _emptyObject);
            }
            
        }

        internal void InteractWithTurret(GameObject turretObj)
        {
            _isTurretPlay = true;
            CameraSignals.Instance.onChangeCameraState?.Invoke(CameraEnums.Turret);
            var playerManagerTransform = _rigidbody.transform;
            Transform turretTransform = turretObj.transform;
            _playerLookAtTurretCommand.Execute(ref playerManagerTransform, ref turretTransform);
            _emptyObject = turretObj;
            
        }

        internal void ExitInteractWithTurret()
        {
            _isTurretPlay = false;
            CameraSignals.Instance.onChangeCameraState?.Invoke(CameraEnums.Start);
        }

       
        
    }
}