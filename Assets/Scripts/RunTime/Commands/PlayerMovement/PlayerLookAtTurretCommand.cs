using DG.Tweening;
using UnityEngine;

namespace RunTime.Commands.PlayerMovement
{
    public class PlayerLookAtTurretCommand
    {
        public void Execute(ref Rigidbody rigidbody, ref GameObject turretObj)
        {
            var playerManagerTransform = rigidbody.transform;
            var turretTransform = turretObj.transform;
            Quaternion newDirect = Quaternion.Euler(playerManagerTransform.eulerAngles.x,turretTransform.eulerAngles.y,playerManagerTransform.eulerAngles.z);
            playerManagerTransform.rotation = newDirect;
            
            var newPos = new Vector3(turretTransform.position.x, playerManagerTransform.position.y,
                turretTransform.position.z);
            playerManagerTransform.DOMove(newPos, 0.1f);
        }
    }
}