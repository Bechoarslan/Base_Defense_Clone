using System;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicController : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Transform stackHolder;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("AmmoArea"))
            {
                playerManager.OnStackAmmo(other.transform,stackHolder,StackType.Ammo);
            }
            else if (other.CompareTag("DepositArea"))
            {
                playerManager.OnSendStackToDeposit(other.transform, stackHolder, StackType.Ammo);
            }
            else if (other.CompareTag("Turret"))
            {
                playerManager.ChangePlayerState(PlayerState.Turret);
                if (stackHolder.childCount > 0)
                {
                    PlayerSignals.Instance.onSendStacksToHolder?.Invoke(stackHolder);
                }
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("AmmoArea"))
            {
               playerManager.StopAllCoroutines();
            }
            else if (other.CompareTag("DepositArea"))
            {
                playerManager.StopAllCoroutines();
            }
            
        }
    }
}