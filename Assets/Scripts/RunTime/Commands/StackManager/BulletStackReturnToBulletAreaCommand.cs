using System.Collections.Generic;
using DG.Tweening;
using RunTime.Enums.Pool;
using RunTime.Signals;
using UnityEngine;

namespace RunTime.Commands.StackManager
{
    public class BulletStackReturnToBulletAreaCommand
    {
        private List<GameObject> _bulletList;

        public BulletStackReturnToBulletAreaCommand(ref List<GameObject> bulletList)
        {
            _bulletList = bulletList;
        }

        public void Execute(ref Transform bulletArea)
        {
            if (_bulletList.Count <= 0) return;
            var area = bulletArea;

            for (var i = _bulletList.Count; i > 0; i--)
            {
                var obj = _bulletList[i - 1];
                _bulletList.Remove(obj);
                obj.transform.parent = bulletArea;
                var randomXPos = Random.Range(-0.5f, 0.5f);
                var randomRot = Random.Range(-180, 180);
                var newPos = new Vector3(obj.transform.position.x + randomXPos, obj.transform.position.y,
                    obj.transform.position.z + randomXPos);
                var newRotation = new Vector3(0, 0, randomRot);
                obj.transform.DOLocalRotate(newRotation, 2f);
                obj.transform.DOMove(newPos, 0.5f).OnComplete(() =>
                {
                    obj.transform.DOMove(area.position, 0.5f).OnComplete(() =>
                    {
                        obj.SetActive(false);
                        PoolSignals.Instance.onSendPool?.Invoke(obj, PoolType.Bullet);
                    });
                });
            }
        }
    }
}