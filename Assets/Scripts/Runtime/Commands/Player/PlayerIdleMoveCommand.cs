using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Player
{
    public class PlayerIdleMoveCommand
    { 
        private Rigidbody _playerRb;
      
        private Vector3 _targetVelocity;
        private PlayerData _playerData;
        private Vector3 _moveVector;
        private Transform _playerTransform;
      
        public PlayerIdleMoveCommand(Rigidbody playerRb, ref CD_PlayerData targetVelocity, Transform playerData)
        {
            _playerRb = playerRb;
            _playerData = targetVelocity.PlayerData;
            _playerTransform = playerData;
        }


        public Vector3 Execute(InputParamsKeys inputParamsKeys)
        {
            _moveVector = new Vector3(inputParamsKeys.InputParams.x, 0, inputParamsKeys.InputParams.y);
            
            if(_moveVector == Vector3.zero)
            {
                _playerRb.velocity = Vector3.zero;
                PlayerSignals.Instance.onChangeAnimFloat?.Invoke(0,PlayerAnimState.Speed);
            }
            else
            {
                PlayerSignals.Instance.onChangeAnimFloat?.Invoke(_playerRb.velocity.magnitude,PlayerAnimState.Speed);
            }
              
         

           
            if (_moveVector.magnitude > 1f) 
            { 
                _moveVector.Normalize(); 
            }
            
            _targetVelocity = _moveVector * _playerData.MoveSpeed;
            _targetVelocity.y = _playerRb.velocity.y;
            _playerRb.velocity = Vector3.Lerp(_playerRb.velocity, _targetVelocity, Time.fixedDeltaTime * 15f);
           
            return _moveVector;
        }

        public void RotatePlayer(Vector3 moveVector)
        {
            if (!(moveVector.sqrMagnitude > 0.1f)) return;
            var targetRotation = Quaternion.LookRotation(moveVector);
            _playerTransform.GetChild(0).rotation =
                Quaternion.Slerp(_playerTransform.GetChild(0).rotation, targetRotation,
                    Time.fixedDeltaTime * _playerData.RotateSpeed);
        }

        public void RotateToEnemy(Vector3 moveVector, Transform lookAtTarget)
        {
            if (lookAtTarget != null)
            {
                Vector3 direction =  lookAtTarget.position - _playerTransform.position;
                direction.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _playerTransform.GetChild(0).rotation =
                    Quaternion.Slerp(_playerTransform.GetChild(0).rotation, targetRotation,
                        Time.fixedDeltaTime * _playerData.RotateSpeed);
            }
            else
            {
                if (!(moveVector.sqrMagnitude > 0.1f)) return;
                var targetRotation = Quaternion.LookRotation(moveVector);
                _playerTransform.GetChild(0).rotation =
                    Quaternion.Slerp(_playerTransform.GetChild(0).rotation, targetRotation,
                        Time.fixedDeltaTime * _playerData.RotateSpeed);
            }
        }
    }
}