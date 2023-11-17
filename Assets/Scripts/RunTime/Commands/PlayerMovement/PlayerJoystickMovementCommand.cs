using RunTime.Controllers.Player;
using RunTime.Data.ValueObject;
using RunTime.Keys;
using Unity.Mathematics;
using UnityEngine;

namespace RunTime.Commands.PlayerMovement
{
    public class PlayerJoystickMovementCommand
    {
        

        public void Execute(ref PlayerMovementData playerMovementData, ref HorizontalInputParams inputParams,
            ref Rigidbody rigidbody)
        {
            var playerMovement = new Vector3(inputParams.Values.x * playerMovementData.JoystickSpeed, 0,
                inputParams.Values.z * playerMovementData.JoystickSpeed);
            rigidbody.velocity = playerMovement;
            if (playerMovement != Vector3.zero)
            {
                Quaternion _newDirect = Quaternion.Euler(rigidbody.rotation.y, Mathf.Atan2(playerMovement.x, playerMovement.z) * Mathf.Rad2Deg, rigidbody.rotation.z);
                rigidbody.rotation = _newDirect;
            }
        }
    }
}