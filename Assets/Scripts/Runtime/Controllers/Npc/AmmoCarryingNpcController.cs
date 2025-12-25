using System.Collections;
using DG.Tweening;
using Runtime.Data.UnityObjects;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Npc
{
    public class AmmoCarryingNpcController : NpcBase
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform ammoStackHolder;

        #endregion

        #region Private Variables
        
 
        #endregion

        #endregion

        protected void Start()
        {
            var ammoHolderPos = StackSignals.Instance.onGetAmmoStackHolderTransform?.Invoke();
            var turretHolderPos = StackSignals.Instance.onGetTurretHolderTransform?.Invoke();
            StartCoroutine(MoveLoop(ammoHolderPos, turretHolderPos));
        }


        public void OnGetStackFromAmmoHolder(Transform otherTransform, Transform stackHolder)
        {
            StartCoroutine(StackSignals.Instance.onSendStackObjectToHolder?.Invoke(otherTransform, stackHolder,StackType.Ammo));
        }

        public void OnSendStackToDeposit(Transform otherTransform, Transform stackHolder)
        {
            StartCoroutine(
                StackSignals.Instance.onSendStackObjectToArea?.Invoke(otherTransform, stackHolder, StackType.Ammo));
        }
    }
}