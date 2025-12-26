using System.Collections;
using Runtime.Enums;
using Runtime.Enums.NPCState;
using Runtime.Managers.NPCManager.Hostage;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Interfaces.BulletCarrierState
{
    public class WaitTakeBulletState : INPCStateMachine
    {
        private NPCBulletCarrierManager Manager;
        
        public WaitTakeBulletState(NPCBulletCarrierManager npcBulletCarrierManager)
        {
            Manager = npcBulletCarrierManager;
        }

        public void EnterState()
        {
            Debug.Log("Taking Bullet's");
            var bulletHolderTransform = GameSignals.Instance.onGetStackAmmoHolderTransform?.Invoke();
            Manager.StartCor(GameSignals.Instance.onSendStackObjectToHolder?.Invoke(bulletHolderTransform,
                Manager.bulletHolder, StackType.Ammo));
            Manager.StartCor(WaitTakeBullet());
        }

        public void UpdateState()
        {
           
        }

        public void OnTriggerEnter(Collider other)
        {
           
        }

        public void OnTriggerExit(Collider other)
        {
            
        }
        
        IEnumerator WaitTakeBullet()
        {
            yield return new WaitForSeconds(Manager.npcData.Data.WaitTime);
            Manager.SwitchState(BulletCarrierType.WalkTurretArea);
        }
    }
}