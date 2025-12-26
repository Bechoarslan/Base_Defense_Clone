using System;
using Runtime.Controllers.NpcController.Hostage;
using Runtime.Data.UnityObjects;
using Runtime.Enums.NPCState;
using Runtime.Interfaces;
using Runtime.Interfaces.HostageState;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Managers.NPCManager.Hostage
{
    public class NPCHostageManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public INPCStateMachine _currentState;
        public Transform playerTransform;

        #endregion
        #region Serialized Variables

        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private NPCHostageController npcHostageController;
        [SerializeField] private CD_NpcData npcHostageData;

        #endregion

        #region Private Variables
        
        private FollowPlayerState _followPlayerState;
        private TerrifiedState _terrifiedState;
        
        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            navMeshAgent.speed = npcHostageData.Data.MoveSpeed;
            _followPlayerState = new FollowPlayerState(this,ref navMeshAgent);
            _terrifiedState = new TerrifiedState(this,ref navMeshAgent);
            _currentState = _terrifiedState;
        }

        private void OnEnable()
        {
            _currentState.EnterState();
        }

        private void OnDisable()
        {
            _currentState = _terrifiedState;
        }

        private void Update()
        {
            _currentState.UpdateState();
        }

        public void SwitchState(HostageStateType hostageStateType)
        {
            switch (hostageStateType)
            {
                case HostageStateType.FollowPlayer:
                    _currentState = _followPlayerState;
                    break;
                case HostageStateType.Terrified:
                    _currentState = _terrifiedState;
                    break;
            }
            _currentState.EnterState();
        }
    }
}