using System;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Npc
{
    public class NpcPhysicController : MonoBehaviour
    {
        [SerializeField] private AmmoCarryingNpcController carryAmmoController;
        [SerializeField] private Transform stackHolder;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("AmmoArea"))
            {
                Debug.Log("Npc Physic");
                carryAmmoController.OnGetStackFromAmmoHolder(other.transform,stackHolder);
            }
            else if (other.CompareTag("DepositArea"))
            {
                carryAmmoController.OnSendStackToDeposit(other.transform, stackHolder);
            }
        }
    }
}