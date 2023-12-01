using System;
using System.Collections;
using RunTime.Enums.NPC;
using RunTime.Enums.Pool;
using RunTime.Interfaces;
using RunTime.Signals;
using RunTime.State;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace RunTime.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private INPCStates iNPC;
        [SerializeField] private NavMeshAgent navMeshAgent;
      

        #endregion

        #region Private Variables

        private EnemyWalkState _enemyWalkState;
        

        #endregion


        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _enemyWalkState = new EnemyWalkState(navMeshAgent,this);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PoolSignals.Instance.onEnemySpawned += OnEnemySpawned;
        }

        private void OnEnemySpawned()
        {
            iNPC = _enemyWalkState;
            iNPC.PerformAction();
        }

        private void UnSubscribeEvents()
        {
            PoolSignals.Instance.onEnemySpawned -= OnEnemySpawned;
        }
        
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}