using RunTime.Data.ValueObject;
using RunTime.Keys;
using UnityEngine;

namespace RunTime.Commands.PlayerMovement
{
    public class PlayerJoystickMovementCommand
    {
        

        public void Execute(ref PlayerMovementData playerMovementData, ref HorizontalInputParams inputParams, ref Rigidbody rigidbody)
        {
            var playerMovement = new Vector3(inputParams.Values.x * playerMovementData.JoystickSpeed, 0,
                inputParams.Values.z * playerMovementData.JoystickSpeed);
            rigidbody.velocity = playerMovement;
            if (playerMovement != Vector3.zero)
            {
                Quaternion _newDirect = Quaternion.LookRotation(playerMovement);
                rigidbody.transform.GetChild(0).rotation = _newDirect;
            }
        }
    }
}