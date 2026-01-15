using System;
using Runtime.Data.UnityObjects;
using Runtime.Enums.NPCState;
using Runtime.Enums.NPCState.MoneyCollector;
using Runtime.Extensions;
using Runtime.Interfaces;
using Runtime.Interfaces.MoneyCollectorState;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Managers.NPCManager.NPCMoneyCollector
{
    public class NPCMoneyCollectorManager : MonoBehaviour
    {
        #region Self Variables

        public IStateMachine currentState = null;
        public Transform waitPoint { get; set; }
        public CD_NpcData npcData;
        public Transform moneyTransform { get; set; }   
        public Transform barrierGate { get; set; }
        #region Serialized Variables

        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] public Transform moneyHolder;
        

        #endregion

        #region Private Variables

        private NPCMoneyCollectorStateType _currentStateType = NPCMoneyCollectorStateType.None;
        

        private CollectorWalkMoneyState _walkMoneyStateState;
        private MoneyCollectorMoveWaitState _moveWaitMoneyStateState;
        
        #endregion

        #endregion

        private void Awake()
        {
            
            GetReferences();
            
        }

        private void Start()
        {
            barrierGate = GameObject.FindGameObjectWithTag("Barrier").transform;
        }

        private void GetReferences()
        {
            _walkMoneyStateState = new CollectorWalkMoneyState(this, ref navMeshAgent);
            _moveWaitMoneyStateState = new MoneyCollectorMoveWaitState(this, ref navMeshAgent);
        }

      

        private void OnEnable()
        {
            OnSwitchState(NPCMoneyCollectorStateType.WaitForMoney);
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GameSignals.Instance.onChangeNPCProperty += OnChangeProperty;
        }

        private void OnChangeProperty(NPCPropertyType property)
        {
            switch (property)
            { 
                case NPCPropertyType.MaxCapacity:
                    npcData.Data.MaxStackCount += 5;
                    break;
                case NPCPropertyType.MaxSpeed:
                    npcData.Data.MoveSpeed += 0.5f;
                    navMeshAgent.speed = npcData.Data.MoveSpeed;
                    break;
                
            }
        }

        private void UnSubscribeEvents()
        {
            GameSignals.Instance.onChangeNPCProperty -= OnChangeProperty;
        }
        
        private void Update()
        {
            currentState.UpdateState();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        public void OnSwitchState(NPCMoneyCollectorStateType newState)
        {
            if(_currentStateType == newState) return;
            if (currentState != null)
            {
                currentState.OnExitState();
            }
            _currentStateType = newState;
          switch (_currentStateType)
            {
                case NPCMoneyCollectorStateType.WaitForMoney:
                    currentState = _moveWaitMoneyStateState;
                    break;
                case NPCMoneyCollectorStateType.WalkMoney:
                    currentState = _walkMoneyStateState;
                    break;
                
                
            }
            
            currentState.EnterState();
        }

    }
}