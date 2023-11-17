using System;
using RunTime.Signals;
using UnityEngine;

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
            
        }

      
    }
}