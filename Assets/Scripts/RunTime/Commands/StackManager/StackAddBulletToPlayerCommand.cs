using System.Collections.Generic;
using DG.Tweening;
using RunTime.Data.UnityObject;
using RunTime.Data.ValueObject;
using RunTime.Enums.Pool;
using RunTime.Signals;
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


        public void Execute(Transform bulletArea)
        {
            for (var i = 0; i < _stackData.StackData[(int)PoolType.Bullet].StackLimit; i++)
            {
               var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Bullet);
                obj.transform.position = bulletArea.position;
                obj.gameObject.SetActive(true);
                obj.transform.parent = _stackManager;
                var newPos = new Vector3(_stackManager.transform.localPosition.x,
                    _stackManager.transform.localPosition.y + _stackData.StackData[(int)PoolType.Bullet].StackOffSet * _stackManager.transform.childCount,
                    0);
                obj.transform.DOLocalMove(newPos, 1f);
                obj.transform.DOLocalRotate(new Vector3(0, 0, 0), 1f);
                _bulletList.Add(obj);
            }


        }
    }
}