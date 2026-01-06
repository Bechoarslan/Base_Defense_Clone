using System;
using DG.Tweening;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicController : MonoBehaviour
    {
        
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Transform stackHolder;
        [SerializeField] private GameObject enemyColliderChecker;
        [SerializeField] private GameObject gunHolder;
        private Transform _barrier;
        private bool _isOpen;
        private bool _isGun;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("AmmoArea"))
            {
                playerManager.OnStackAmmo(other.transform,stackHolder,StackType.Ammo);
            }
            else if (other.CompareTag("DepositArea"))
            {
                playerManager.OnSendStackToDeposit(other.transform, stackHolder, StackType.Ammo);
            }
            else if (other.CompareTag("Turret"))
            {
                playerManager.OnStateChanged(PlayerState.Turret);
                GameSignals.Instance.onTurretStateChange?.Invoke(TurretState.PlayerIn);
                
                if (stackHolder.childCount > 0)
                {
                    PlayerSignals.Instance.onSendStacksToHolder?.Invoke(stackHolder);
                }
            }
            else if (other.CompareTag("GunOff"))
            {
                OnEnterAndExitOutOfBase(true);

            }
            else if (other.CompareTag("GunOn")) 
            {
                OnEnterAndExitOutOfBase(false);
            }
            else if (other.CompareTag("InOutOfBase"))
            {
                var block = GameObject.FindGameObjectWithTag("Barrier");
                RotateGate(block);
            }
            else if (other.CompareTag("Hostage"))
            {
                playerManager.HostageList.Add(other.gameObject.transform.parent.gameObject);
            }
            else if (other.CompareTag("Mine"))
            {
                if (playerManager.HostageList.Count <= 0) return;
                foreach (var hostages in playerManager.HostageList)
                {
                    PlayerSignals.Instance.onPlayerEnteredMineArea?.Invoke(hostages);
                }
                
            }
          
           
        }

        private void OnEnterAndExitOutOfBase(bool value)
        {
           
            if (!value)
            {
                
                playerManager.OnStateChanged(PlayerState.Shooting);
                enemyColliderChecker.layer = LayerMask.NameToLayer("Targetable");
                playerManager.SetHealthVisible(true);
                gunHolder.gameObject.SetActive(true);
                
             
            }
            else
            {
                
                playerManager.OnStateChanged(PlayerState.Idle);
                enemyColliderChecker.layer = LayerMask.NameToLayer("Player");
                playerManager.SetHealthVisible(false);
                gunHolder.gameObject.SetActive(false);
           
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("AmmoArea"))
            {
               playerManager.StopAllCoroutines();
            }
            else if (other.CompareTag("DepositArea"))
            {
                playerManager.StopAllCoroutines();
            }
            else if (other.CompareTag("Turret"))
            {
                GameSignals.Instance.onTurretStateChange?.Invoke(TurretState.None);
            }
            else if (other.CompareTag("InOutOfBase"))
            {
                var block = GameObject.FindGameObjectWithTag("Barrier");
                RotateGate(block);
            }
            
          
            
        }

        private void RotateGate(GameObject blokc)
        {
            blokc.transform.DOLocalRotate(
                _isOpen ? new Vector3(0, 0, 0) : new Vector3(0, 0, 90),
                0.4f
            );

            _isOpen = !_isOpen;
        }
    }
}