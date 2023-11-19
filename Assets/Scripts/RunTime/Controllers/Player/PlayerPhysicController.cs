using System;
using RunTime.Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace RunTime.Controllers.Player
{
   
    public class PlayerPhysicController : MonoBehaviour
    {
        [SerializeField] private Transform itemHolder;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Turret"))
            {
                PlayerSignals.Instance.onPLayerInteractWithTurret?.Invoke(other.transform.gameObject);
            }

            if (other.CompareTag("BulletArea"))
            {
                StackSignals.Instance.onPlayerInteractWithBulletArea?.Invoke(other.transform.parent.gameObject.transform,itemHolder);
            }
            
        }

      
    }
}