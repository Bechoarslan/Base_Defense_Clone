using System;
using RunTime.Commands.PlayerMovement;
using RunTime.Data.ValueObject;
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
        [ShowInInspector]private bool _isReadyToMove, _isReadyToPlay;

        private PlayerJoystickMovementCommand _playerJoystickMovementCommand;
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
            
        }

        
    }
}