using System.Collections;
using Runtime.Enums;
using Runtime.Enums.NPCState;
using Runtime.Managers.NPCManager.Hostage;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Interfaces.BulletCarrierState
{
    public class WaitDepositTurretState : IStateMachine
    {
        private NPCBulletCarrierManager Manager;
        public WaitDepositTurretState(NPCBulletCarrierManager npcBulletCarrierManager)
        {
            Manager = npcBulletCarrierManager;
        }

        public void EnterState()
        {
            
            Manager.OnSetTriggerAnim("Idle");
          
            Manager.StartCor(GameSignals.Instance.onSendBulletStackObjectToArea?.Invoke(Manager.holder,Manager.bulletHolder,StackType.Ammo));
            Manager.StartCor(WaitForDeposit());
        }

        public void UpdateState()
        {
            
        }

        public void OnStateTriggerEnter(Collider other)
        {
            
        }

        public void OnStateTriggerExit(Collider other)
        {
            
            
        }

        public void OnExitState()
        {
            Manager.holder = null;
        }

        IEnumerator WaitForDeposit()
        {
            yield return new WaitForSeconds(Manager.npcData.Data.WaitTime);
            Manager.SwitchState(BulletCarrierStateType.WalkAmmoArea);
        }
    }
}