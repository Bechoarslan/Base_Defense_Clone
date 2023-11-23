using System;
using RunTime.Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace RunTime.Controllers.Player
{
   
    public class PlayerPhysicController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Turret"))
            {
                PlayerSignals.Instance.onPLayerInteractWithTurret?.Invoke(other.transform.gameObject);
            }

            if (other.CompareTag("BulletArea"))
            {
                StackSignals.Instance.onPlayerInteractWithBulletArea?.Invoke(other.transform);
            }

            if (other.CompareTag("TurretBulletArea"))
            {
                
                StackSignals.Instance.onPlayerInteractWithTurretBulletArea?.Invoke(other.transform);
            }
            
        }

      
    }
}