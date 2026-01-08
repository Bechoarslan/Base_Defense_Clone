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
        private PoolType _poolType;
        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "AmmoArea":
                    playerManager.OnStackAmmo(other.transform, stackHolder, StackType.Ammo);
                    _poolType = PoolType.Ammo;
                    break;

                case "Money":
                    _poolType = PoolType.Money;
                    playerManager.OnGetMoneyStack(stackHolder, other.gameObject);
                    break;
                case "DepositArea":
                    playerManager.OnSendStackToDeposit(other.transform, stackHolder, StackType.Ammo);
                    break;

                case "Turret":
                    playerManager.OnStateChanged(PlayerState.Turret);
                    GameSignals.Instance.onTurretStateChange?.Invoke(TurretState.PlayerIn);

                    if (stackHolder.childCount > 0)
                    {
                        PlayerSignals.Instance.onSendStacksToHolder?.Invoke(stackHolder,_poolType);
                    }
                    break;

                case "GunOff":
                    OnEnterAndExitOutOfBase(true);
                    if(stackHolder.childCount > 0)
                        PlayerSignals.Instance.onSendStacksToHolder?.Invoke(stackHolder,_poolType);
                    break;

                case "GunOn":
                    if(stackHolder.childCount > 0)
                        PlayerSignals.Instance.onSendStacksToHolder?.Invoke(stackHolder,_poolType);
                    OnEnterAndExitOutOfBase(false);
                    break;

                case "InOutOfBase":
                    var block = GameObject.FindGameObjectWithTag("Barrier");
                    RotateGate(block);
                    break;

                case "Hostage":
                    playerManager.HostageList.Add(other.transform.parent.gameObject);
                    break;

                case "Mine":
                    if (playerManager.HostageList.Count <= 0) break;

                    foreach (var hostages in playerManager.HostageList)
                    {
                        PlayerSignals.Instance.onPlayerEnteredMineArea?.Invoke(hostages);
                        playerManager.EnemyList.Remove(hostages);
                    }
                    break;
                case "GemStack":
                    if (other.gameObject.transform.childCount > 0)
                    {
                        for (int i = other.gameObject.transform.childCount; i > 0 ; i--)
                        {
                            var gem = other.gameObject.transform.GetChild(i - 1).gameObject;
                           
                            gem.transform.DOLocalJump( new Vector3(0, 2f, 0), 1f, 1, 0.5f).OnComplete(() =>
                            { 
                                gem.transform.parent = null;
                                PoolSignals.Instance.onSendPoolObject?.Invoke(gem,PoolType.Gem);
                                GameSignals.Instance.onGetResourceKeys?.Invoke().AddGem(1);
                            });
                            
                         
                        }
                    }
                    break;
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
            switch (other.tag)
            {
                case "AmmoArea":
                    playerManager.StopAllCoroutines();
                    break;
                case "DepositArea":
                    playerManager.StopAllCoroutines();
                    break;
                case "Turret":
                    GameSignals.Instance.onTurretStateChange?.Invoke(TurretState.None);
                    break;
                case "InOutOfBase":
                    var block = GameObject.FindGameObjectWithTag("Barrier");
                    RotateGate(block);
                    break;
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