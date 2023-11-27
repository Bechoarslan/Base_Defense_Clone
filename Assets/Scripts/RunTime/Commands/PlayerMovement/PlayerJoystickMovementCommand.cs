using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RunTime.Controllers.Player;
using RunTime.Data.ValueObject;
using RunTime.Keys;
using Unity.Mathematics;
using UnityEngine;

namespace RunTime.Commands.PlayerMovement
{
    public class PlayerJoystickMovementCommand
    {
        
        public void Execute(ref PlayerData playerData, ref HorizontalInputParams inputParams,
            ref Rigidbody rigidbody)
        {
            var playerMovement = new Vector3(inputParams.Values.x * playerData.JoystickSpeed, 0,
                inputParams.Values.z * playerData.JoystickSpeed);
            rigidbody.velocity = playerMovement;
            if(playerMovement != Vector3.zero)
            {
                Quaternion newDirect = Quaternion.LookRotation(playerMovement);
                rigidbody.transform.rotation = newDirect;
            }
            

        }
        
        
        
        
        
    }
}