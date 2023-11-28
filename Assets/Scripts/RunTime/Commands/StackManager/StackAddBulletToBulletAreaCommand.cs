using System.Collections.Generic;
using DG.Tweening;
using RunTime.Data.ValueObject;
using RunTime.Enums.Pool;
using UnityEngine;

namespace RunTime.Commands.StackManager
{
    public class StackAddBulletToBulletAreaCommand
    {
        private readonly List<Vector3> _stackData;
        private readonly List<GameObject> _bulletList;
        public StackAddBulletToBulletAreaCommand(ref PlayerData stackData, ref List<GameObject> bulletList)
        {
            _stackData = stackData.StackData[(int)PoolType.Bullet].BulletData;
            _bulletList = bulletList;
        }


        public void Execute(ref Transform turretArea)
        {
            for (int i =  _bulletList.Count; i > 0 ; i--)
            {
                var areaChildCount = turretArea.childCount;
                var stackValue = areaChildCount / 4;
                var obj = _bulletList[^1];
                var index = areaChildCount % _stackData.Count;
                
                
                obj.transform.parent = turretArea;
                var newPos = new Vector3(_stackData[index].x, _stackData[index].y + stackValue * 0.500f, _stackData[index].z);
                obj.transform.DOLocalMove(newPos, 1f);
                obj.transform.DORotate(Vector3.zero, 1f);
                _bulletList.Remove(obj);
                
            }
        }
    }
}