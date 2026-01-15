using System;
using Runtime.Managers.EnemyManager;
using UnityEngine;

namespace Runtime.Controllers.NpcController.EnemyBoss
{
    public class EnemyBossController : MonoBehaviour
    {
        [SerializeField] private EnemyBossManager bossManager;

        private void OnTriggerEnter(Collider other)
        {
            bossManager._currentState.OnStateTriggerEnter(other);
        }
        
        private void OnTriggerExit(Collider other) 
        {
            bossManager._currentState.OnStateTriggerExit(other);
        }
    }
}