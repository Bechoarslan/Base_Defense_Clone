using System.Collections;
using Runtime.Enums;
using Runtime.Enums.NPCState;
using Runtime.Managers.NPCManager.Hostage;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Interfaces.HostageState
{
    public class HostageMineAndCarryGemState : IStateMachine
    { 
        private NPCHostageManager Manager;
        private NavMeshAgent Agent;
        private Coroutine _mineCoroutine;
        public HostageMineAndCarryGemState(NPCHostageManager npcHostageManager, ref NavMeshAgent navMeshAgent)
        {
            Manager = npcHostageManager;
            Agent = navMeshAgent;
        }

        public void EnterState()
        {
            _mineCoroutine = Manager.StartCoroutine(MineAndCarryCoroutine());
        }

        private IEnumerator MineAndCarryCoroutine()
        {
            Manager.pickaxeObj.SetActive(true);
            Manager.SetTriggerAnimation(nameof(HostageAnimState.Mine));
            yield return new WaitForSeconds(5f);
            GetMine();
           Manager.SetTriggerAnimation(nameof(HostageAnimState.Carry));
            Manager.pickaxeObj.SetActive(false);
            var gemTransform = GameSignals.Instance.onGetGemStackAreaTransform.Invoke();
            Agent.destination = gemTransform.position;


        }

        private void GetMine()
        {
            var gemObj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Gem);
            gemObj.transform.parent = Manager.gemHolderTransform;
            gemObj.transform.localPosition = Vector3.zero;
            gemObj.SetActive(true);
        }

        public void UpdateState()
        {
            
        }

        public void OnStateTriggerEnter(Collider other)
        {
         
            if (other.CompareTag("GemStack"))
            {
                GameSignals.Instance.onSendGemToHolder?.Invoke(Manager.gemHolderTransform.GetChild(0).gameObject);
                Manager.SwitchState(NPCHostageStateType.Mine);
                Debug.Log("Hostage Delivired Gem");
            }
        }

        public void OnStateTriggerExit(Collider other)
        {
            
        }

        public void OnExitState()
        {
            Manager.StopCoroutine(_mineCoroutine);
        }
    }
}