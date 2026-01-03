using System;
using Runtime.Commands.Player;
using Runtime.Data.UnityObjects;
using Runtime.Enums;
using Runtime.Keys;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private PlayerShootingController playerShootingController;
        [SerializeField] private Rigidbody playerRb;


        #endregion


        #region Commands

        private PlayerIdleMoveCommand _idleMoveCommand;
        private PlayerTurretMoveCommand _playerTurretMoveCommand;

        #endregion
        #region Private Variables

        private CD_PlayerData _playerData;
        private InputParamsKeys _inputParamsKeys;
        private PlayerState _playerState;
        
        private Transform _turretTransform;
        private Transform _turretStandPoint;
        
        private bool _isInTurret;
        

        #region Ref Values
        private Vector3 _moveVector;

        #endregion
       
        #endregion

        #endregion

        private void Start()
        {
            InitCommand();
        }

        private void InitCommand()
        {
            _idleMoveCommand = new PlayerIdleMoveCommand(playerRb,ref _playerData,transform);
            _playerTurretMoveCommand = new PlayerTurretMoveCommand(playerRb, transform, _playerData);

        }

        public void GetPlayerData(CD_PlayerData playerData) => _playerData = playerData;
       
        private void FixedUpdate()
        {
            switch (_playerState)
            {
                case PlayerState.Idle: 
                    _moveVector = _idleMoveCommand.Execute(_inputParamsKeys);
                   _idleMoveCommand.RotatePlayer(_moveVector);
                    break;
                case PlayerState.Turret:
                    RotateTurret();
                    break;
                case PlayerState.Shooting:
                    _moveVector = _idleMoveCommand.Execute(_inputParamsKeys);
                    _idleMoveCommand.RotateToEnemy(_moveVector,playerShootingController.EnemyTarget);
                    break;
            }
           
        }

        private void RotateTurret()
        {
            if (!_isInTurret) return;
            playerRb.velocity = Vector3.zero;
            if (_inputParamsKeys.InputParams.sqrMagnitude > 0.1f)
            {
                Vector3 dir = new Vector3(
                    _inputParamsKeys.InputParams.x,
                    0f,
                    _inputParamsKeys.InputParams.y
                );
                if (_inputParamsKeys.InputParams.y < -0.8)
                {
                    
                    GetOutFromTurret();
                  _isInTurret = false;
                    return;
                }
                _playerTurretMoveCommand.Execute(_turretStandPoint,dir,_turretTransform);
            }
         
        }

        private void GetOutFromTurret()
        {
            _turretStandPoint = null;
            _turretTransform = null;
            PlayerSignals.Instance.onChangeAnimBool?.Invoke(false,PlayerAnimState.IsHolding);
            PlayerSignals.Instance.onChangePlayerState?.Invoke(PlayerState.Idle);
        }

        
        public void OnInputChanged(InputParamsKeys inputParams) => _inputParamsKeys = inputParams;
        
        public void OnSetTurretPos()
        {
            (_turretTransform, _turretStandPoint) = Signals.GameSignals.Instance.onGetTurretStandPointAndTurretTransform();
                    
            var newPos = new Vector3(_turretStandPoint.position.x,transform.position.y, _turretStandPoint.position.z);
            transform.localPosition = newPos;
            transform.localRotation = Quaternion.Euler(_turretStandPoint.localRotation.eulerAngles);
            _isInTurret = true;
        }

        public void OnStateChanged(PlayerState playerState) => _playerState = playerState;
        
    }
}