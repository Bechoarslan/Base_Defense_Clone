using Runtime.Enums.EnemyStateType;
using Runtime.Managers.EnemyManager;
using UnityEngine;

namespace Runtime.Controllers.NpcController.Enemy
{
    public class EnemyPhysicController : MonoBehaviour
    {
        [SerializeField] private EnemyManager enemyManager;

        private void OnTriggerEnter(Collider other)
        {
            enemyManager._currentState.OnStateTriggerEnter(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            enemyManager._currentState.OnStateTriggerExit(other);
        }
    }
}