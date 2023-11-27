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
                PlayerSignals.Instance.onPlayerInteractWithBulletArea?.Invoke(other.transform);
            }

            if (other.CompareTag("TurretBulletArea"))
            {
                
                PlayerSignals.Instance.onPlayerInteractWithTurretBulletArea?.Invoke(other.transform);
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Turret"))
            {
                PlayerSignals.Instance.onPlayerExitInteractWithTurret?.Invoke();
            }
        }
    }
}