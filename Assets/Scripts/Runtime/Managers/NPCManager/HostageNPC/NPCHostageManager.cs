using System;
using Runtime.Controllers.NpcController;
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

        public IStateMachine _currentState;
        public Transform playerTransform;

        #endregion
        #region Serialized Variables

        [SerializeField] private NPCAnimationController npcAnimationController;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private NPCHostageController npcHostageController;
        [SerializeField] private CD_NpcData npcHostageData;

        #endregion

        #region Private Variables
        
        private FollowPlayerState _followPlayerState;
        private TerrifiedState _terrifiedState;
        private MoveToMineState _moveToMineState;
        
        private NPCHostageStateType _currentStateType;
        
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
            _moveToMineState = new MoveToMineState(this,ref navMeshAgent);
            _currentStateType = NPCHostageStateType.Terrified;
        }

        private void OnEnable()
        {
            SwitchState(_currentStateType);
        }

        private void OnDisable()
        {
            _currentState = _terrifiedState;
        }

        private void Update()
        {
            _currentState.UpdateState();
        }

        public void SwitchState(NPCHostageStateType npcHostageStateType)
        {
            if (_currentState != null)
            {
               
               _currentState.OnExitState();
            }
            _currentStateType = npcHostageStateType;
            switch (_currentStateType)
            {
                case NPCHostageStateType.FollowPlayer:
                    _currentState = _followPlayerState;
                    break;
                case NPCHostageStateType.Terrified:
                    _currentState = _terrifiedState;
                    break;
                case NPCHostageStateType.Mine:
                    _currentState = _moveToMineState;
                    break;
            }
            _currentState.EnterState();
        }
        
        public void SetTriggerAnimation(string animationName)
        {
        
            npcAnimationController.OnTriggerAnimation(animationName);
        }
        
        public void SetBoolAnimation(string animationName, bool value)
        {
            npcAnimationController.OnChangeBoolAnimation(animationName, value);
        }
    }
}