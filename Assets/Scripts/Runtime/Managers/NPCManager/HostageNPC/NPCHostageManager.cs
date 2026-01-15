using System;
using Runtime.Controllers.NpcController;
using Runtime.Controllers.NpcController.Hostage;
using Runtime.Data.UnityObjects;
using Runtime.Enums;
using Runtime.Enums.NPCState;
using Runtime.Interfaces;
using Runtime.Interfaces.HostageState;
using Runtime.Signals;
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
        public GameObject pickaxeObj;
        public Transform gemHolderTransform;
        public Transform mineAreaTransform;
        public GemMineType gemMineType;

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
        private HostageMineAndCarryGemState _mineAndMoveToGemAreaState;
        private NPCHostageStateType _currentStateType = NPCHostageStateType.None;
       
        
        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
           _followPlayerState = new FollowPlayerState(this,ref navMeshAgent);
            _terrifiedState = new TerrifiedState(this,ref navMeshAgent);
            _moveToMineState = new MoveToMineState(this,ref navMeshAgent);
            _mineAndMoveToGemAreaState = new HostageMineAndCarryGemState(this,ref navMeshAgent);
            SwitchState(NPCHostageStateType.Terrified);
        }

        private void OnEnable()
        {
            SwitchState(_currentStateType);
            GameSignals.Instance.onChangeNPCProperty += OnChangeProperty;
        }

        private void OnChangeProperty(NPCPropertyType property)
        {
            switch (property)
            { 
                case NPCPropertyType.MaxCapacity:
                    npcHostageData.Data.MaxStackCount += 5;
                    break;
                case NPCPropertyType.MaxSpeed:
                    npcHostageData.Data.MoveSpeed += 0.5f;
                    navMeshAgent.speed = npcHostageData.Data.MoveSpeed;
                    break;

            }
        }
        private void OnDisable()
        {
            _currentState = _terrifiedState;
            GameSignals.Instance.onChangeNPCProperty += OnChangeProperty;
        }

        private void Update()
        {
            _currentState.UpdateState();
        }

        public void SwitchState(NPCHostageStateType npcHostageStateType)
        {
            if(_currentStateType == npcHostageStateType) return;
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
                case NPCHostageStateType.MineAndCarryGem:
                    _currentState = _mineAndMoveToGemAreaState;
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