using System;
using DG.Tweening;
using RunTime.Controllers.Player;
using RunTime.Data.ValueObject;
using RunTime.Enums;
using RunTime.Enums.Camera;
using RunTime.Keys;
using RunTime.Signals;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.UI;

namespace RunTime.Commands.PlayerMovement
{
    public class PlayerTurretMovementCommand
    {
        public void Execute(ref HorizontalInputParams inputParams,
            ref Rigidbody rigidbody, ref GameObject emptyObject, ref bool isTurretPlay)
        {
            var newRotation = inputParams.Values.x * 25;
            if(newRotation == 0) return;
            emptyObject.transform.parent.gameObject.transform.rotation = Quaternion.Euler(0,newRotation,0);


            var transform = rigidbody.transform;
            var newPos = new Vector3(emptyObject.transform.position.x, transform.position.y,
                emptyObject.transform.position.z);
            transform.position = newPos;
            
            
            Quaternion newDirect = Quaternion.Euler(rigidbody.rotation.x,emptyObject.transform.parent.gameObject.transform.eulerAngles.y,transform.rotation.z);
            transform.rotation = newDirect;
            
            
            
            var escapeValue = Math.Clamp(inputParams.Values.z, -1, 0);
            if (!(escapeValue < -0.8f)) return;
            isTurretPlay = false;
            rigidbody.velocity = new Vector3(0, 0, -10);




        }
            
            
            }
        }

    
