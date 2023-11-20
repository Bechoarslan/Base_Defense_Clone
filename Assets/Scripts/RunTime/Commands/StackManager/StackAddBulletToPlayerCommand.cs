using System.Collections.Generic;
using DG.Tweening;
using RunTime.Data.UnityObject;
using RunTime.Enums.Pool;
using RunTime.Signals;
using UnityEngine;

namespace RunTime.Commands.StackManager
{
    public class StackAddBulletToPlayerCommand
    {
        private List<GameObject> _bulletList;
        private CD_StackData _stackData;
        
        public StackAddBulletToPlayerCommand(ref List<GameObject> bulletList, ref CD_StackData stackData)
        {
            _bulletList = bulletList;
            _stackData = stackData;
        }

        public void Execute(Transform bulletArea, Transform playerManager)
        {
            for (var i = 0; i < _stackData.Data[(int)PoolType.Bullet].StackLimit - 1; i++)
            {
               var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Bullet);
                obj.transform.position = bulletArea.position;
                obj.gameObject.SetActive(true);
                obj.transform.parent = playerManager;
                var newPos = new Vector3(playerManager.localPosition.x,
                    playerManager.localPosition.y + _stackData.Data[(int)PoolType.Bullet].StackOffSet * playerManager.childCount,
                    playerManager.localPosition.z);
                obj.transform.DOLocalMove(newPos, 2f);
                obj.transform.DOLocalRotate(new Vector3(0, 0, 0), 1.5f);
            }


        }
    }
}