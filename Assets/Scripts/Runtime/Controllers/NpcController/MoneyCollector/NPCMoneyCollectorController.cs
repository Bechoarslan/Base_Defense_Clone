using System;
using Runtime.Managers.NPCManager.NPCMoneyCollector;
using UnityEngine;

namespace Runtime.Controllers.NpcController.MoneyCollector
{
    public class NPCMoneyCollectorController : MonoBehaviour
    {
        [SerializeField] private NPCMoneyCollectorManager _controller;

        private void OnTriggerEnter(Collider other)
        {
            _controller.currentState.OnStateTriggerEnter(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            _controller.currentState.OnStateTriggerExit(other);
        }
    }
}