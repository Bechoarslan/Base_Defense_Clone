using System.Collections;
using DG.Tweening;
using Runtime.Enums;
using Runtime.Enums.EnemyStateType;
using Runtime.Managers.EnemyManager;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Interfaces.BossEnemy
{
    public class EnemyBossStartThrowGrenade : IStateMachine
    {
        private EnemyBossManager Manager;
        private Coroutine grenadeCoroutine;
        public EnemyBossStartThrowGrenade(EnemyBossManager enemyBossManager)
        {
            Manager = enemyBossManager;
        }

        public void EnterState()
        {
           
            grenadeCoroutine = Manager.StartCoroutine(StartThrowingGrenade());
        }

        private IEnumerator StartThrowingGrenade()
        {
            var waiter = new WaitForSeconds(3f);
            while (true)
            {
             
                yield return waiter;
                var hitbox = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Hitbox);
                hitbox.transform.parent = null;
                var hitboxDirection = Manager.target.position;
                
                hitboxDirection.y = 0.5f;
                hitbox.transform.position = hitboxDirection;
                hitbox.SetActive(true);
                Manager.AnimationSetTrigger(EnemyStateType.Throw);
                yield return new WaitForSeconds(0.2f);
                ThrowGrenade(hitboxDirection,hitbox);
               
               
                yield return new WaitForSeconds(2f);
            }
        }

        private void ThrowGrenade(Vector3 hitboxDirection, GameObject hitbox)
        {
            var grenade = Manager.grenadeHolder.GetChild(0);
            float throwDuration = 1.2f;
            Vector3 startPos = Manager.grenadeHolder.position;
            startPos.y = 5f;
            Vector3 midPos = (startPos + hitboxDirection) * 0.5f;
            midPos.y += 0.2f;

            Vector3[] path = new Vector3[]
            {
                startPos,
                midPos,
                Manager.target.position
            };

            grenade.transform
                .DOPath(path, throwDuration, PathType.CatmullRom)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                   grenade.gameObject.SetActive(false);
                   grenade.transform.localPosition = Vector3.zero;
                   grenade.gameObject.SetActive(true);
                   PoolSignals.Instance.onSendPoolObject?.Invoke(hitbox,PoolType.Hitbox);
                });
            
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
           if(grenadeCoroutine != null)
                Manager.StopCoroutine(grenadeCoroutine);
        }
    }
}