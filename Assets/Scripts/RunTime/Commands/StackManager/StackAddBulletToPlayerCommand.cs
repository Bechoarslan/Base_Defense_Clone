using System.Collections.Generic;
using DG.Tweening;
using RunTime.Data.UnityObject;
using RunTime.Data.ValueObject;
using RunTime.Enums.Pool;
using RunTime.Signals;
using Unity.VisualScripting;
using UnityEngine;

namespace RunTime.Commands.StackManager
{
    public class StackAddBulletToPlayerCommand
    {
        private PlayerData _stackData;
        private Transform _stackManager;
        private List<GameObject> _bulletList;

        public StackAddBulletToPlayerCommand(ref PlayerData stackData, Transform stackManager,
            ref List<GameObject> bulletList)
        {
            _stackData = stackData;
            _stackManager = stackManager;
            _bulletList = bulletList;
            
        }


        public void Execute(ref Transform bulletArea, ref bool isGotStack)
        {
            if (isGotStack) return;
            for (var i = 0; i < _stackData.StackData[(int)PoolType.Bullet].StackLimit; i++)
            {
                var bullet = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Bullet);
                bullet.transform.position = new Vector3(bulletArea.position.x + Random.Range(-0.5f, 0.5f),
                    bulletArea.position.y + Random.Range(-0.5f, 0.5f), bulletArea.position.z + Random.Range(-0.5f, 0.5f));
                bullet.SetActive(true);
                var posX = _stackManager.localPosition.x;
                var posY = _stackManager.localPosition.y +(_stackData.StackData[(int)PoolType.Bullet].StackOffSet * (_bulletList.Count % 10));
                var poZ = _stackManager.localPosition.z - _bulletList.Count / 10 * 0.6f;
                bullet.transform.parent = _stackManager;
                var newPos = new Vector3(posX, posY, poZ);
                bullet.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
                bullet.transform.DOLocalMove(newPos, 1f);
                _bulletList.Add(bullet);
                
                
            }
           
            
           


        }
    }
}