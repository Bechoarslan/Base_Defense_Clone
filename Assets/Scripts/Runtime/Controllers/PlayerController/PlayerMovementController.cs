using Runtime.Data.UnityObjects;
using Runtime.Enums;
using Runtime.Keys;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        
        [SerializeField] private Rigidbody playerRb;
        [SerializeField] private PlayerAnimationController playerAnimationController;


        #endregion

        #region Private Variables

        private CD_PlayerData _playerData;
        private Vector3 _moveVector;
        private InputParamsKeys _inputParamsKeys;
        private PlayerState _playerState;
        
        private Transform _turretTransform;
        private Transform _turretStandPoint;
        
        private bool _isInTurret;
        #endregion

        #endregion

        public void GetPlayerData(CD_PlayerData playerData) => _playerData = playerData;
       
        private void FixedUpdate()
        {
            switch (_playerState)
            {
                case PlayerState.Idle:
                    Move();
                    RotateCharacter();
                    break;
                case PlayerState.Turret:
                    RotateTurret();
                    break;
                case PlayerState.Shooting:
                    Move();
                    RotateCharacter();
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
                    _isInTurret = false;
                    GetOutFromTurret();
                    return;
                }
                
                float targetYAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                float clampedYAngle = Mathf.Clamp(targetYAngle, -45f, 45f);

                Quaternion targetRotation = Quaternion.Euler(0f, clampedYAngle, 0f);
                playerRb.MovePosition(new Vector3(_turretStandPoint.position.x,
                    transform.position.y, _turretStandPoint.position.z));
                playerRb.MoveRotation(targetRotation);
               
                _turretTransform.rotation = Quaternion.Slerp(
                    _turretTransform.rotation,
                    targetRotation,
                    Time.fixedDeltaTime * _playerData.PlayerData.RotateSpeed
                );
                
            }
        }

        private void GetOutFromTurret()
        {
            _turretStandPoint = null;
            _turretTransform = null;
            transform.SetParent(null);
            OnStateChanged(PlayerState.Idle);
        }

        public void OnInputChanged(InputParamsKeys inputParams)
        {
            _inputParamsKeys = inputParams;
        }


        private void Move()
        {
            _moveVector = new Vector3(_inputParamsKeys.InputParams.x, 0, _inputParamsKeys.InputParams.y);

            // 2. Animasyon kontrolü
            if(_moveVector == Vector3.zero)
            {
                playerAnimationController.OnChangeAnimationBool(false, PlayerAnimState.IsRunning);
            }
            else
            {
                playerAnimationController.OnChangeAnimationBool(true, PlayerAnimState.IsRunning);
            }

           
            if (_moveVector.magnitude > 1f) 
            { 
                _moveVector.Normalize(); 
            }
            
            Vector3 currentVelocity = playerRb.velocity;
            Vector3 targetVelocity = _moveVector * _playerData.PlayerData.MoveSpeed;
            targetVelocity.y = currentVelocity.y;
            playerRb.velocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.fixedDeltaTime * 15f);
        }
        

        private void RotateCharacter()
        {
            if (_moveVector.sqrMagnitude > 0.1f)
            {
                var targetRotation = Quaternion.LookRotation(_moveVector);
                transform.GetChild(0).rotation =
                    Quaternion.Slerp(transform.GetChild(0).rotation, targetRotation, Time.fixedDeltaTime * _playerData.PlayerData.RotateSpeed);
            }
        }


        public void OnStateChanged(PlayerState playerState)
        {
            if (this._playerState == playerState) return;
            switch (playerState)
            {
                case PlayerState.Idle:
                    playerAnimationController.OnChangeAnimationBool(false,PlayerAnimState.IsHolding);
                    playerAnimationController.OnChangeBaseLayer(1, 0);
                    break;
                case PlayerState.Turret:
                    playerAnimationController.OnChangeAnimationBool(true,PlayerAnimState.IsHolding);
                    (_turretTransform, _turretStandPoint) = Signals.GameSignals.Instance.onGetTurretStandPointAndTurretTransform();
                    
                    var newPos = new Vector3(_turretStandPoint.position.x,transform.position.y, _turretStandPoint.position.z);
                    transform.localPosition = newPos;
                    transform.localRotation = Quaternion.Euler(_turretStandPoint.localRotation.eulerAngles);
                    _isInTurret = true;
                    break;
                case PlayerState.Shooting:
                    playerAnimationController.OnChangeBaseLayer(1,1f);
                    
                    break;
            }
            this._playerState = playerState;
        }
    }
}