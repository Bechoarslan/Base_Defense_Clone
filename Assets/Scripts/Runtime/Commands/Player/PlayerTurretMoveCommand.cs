using Runtime.Data.UnityObjects;
using Runtime.Enums;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;


namespace Runtime.Commands.Player
{
    public class PlayerTurretMoveCommand
    { 
     
       
        private Rigidbody playerRb;
        private Transform _playerTransform;
        private CD_PlayerData _playerData;
        
        
       

        public PlayerTurretMoveCommand(Rigidbody rigidbody, Transform transform, CD_PlayerData playerData)
        {
            playerRb = rigidbody;
            _playerTransform = transform;
            _playerData = playerData;
        }


        public  void Execute(Transform turretStandPoint,Vector3 dir,Transform turretTransform)
        {
            
            var targetYAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            var clampedYAngle = Mathf.Clamp(targetYAngle, -45f, 45f);

            Quaternion targetRotation = Quaternion.Euler(0f, clampedYAngle, 0f);
            playerRb.MovePosition(new Vector3(turretStandPoint.position.x,
                _playerTransform.position.y, turretStandPoint.position.z));
            playerRb.MoveRotation(targetRotation);
               
            turretTransform.rotation = Quaternion.Slerp(
                turretTransform.rotation,
                targetRotation,
                Time.fixedDeltaTime * _playerData.PlayerData.RotateSpeed
            );
        }
    }
}