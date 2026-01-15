using Runtime.Enums.EnemyStateType;
using Runtime.Managers.EnemyManager;
using UnityEngine;

namespace Runtime.Interfaces.BossEnemy
{
    public class EnemyBossWaitState : IStateMachine
    {
        private EnemyBossManager Manager;
        public EnemyBossWaitState(EnemyBossManager enemyBossManager)
        {
            Manager = enemyBossManager;
        }

        public void EnterState()
        {
          Manager.AnimationSetTrigger(EnemyStateType.Idle);
        }

        public void UpdateState()
        {
           
        }

        public void OnStateTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log(other.transform.parent.name);
                Manager.target = other.transform.parent;
                Manager.SwitchState(EnemyBossStateType.BossThrowGrenadeState);
            }
        }

        public void OnStateTriggerExit(Collider other)
        {
          
        }

        public void OnExitState()
        {
            
        }
    }
}