using RunTime.Data.UnityObject;
using UnityEngine;

namespace RunTime.Commands.Pool
{
    public class PoolGenerateCommand
    {
        private CD_PoolData _poolData;
        private Transform _poolHolder;
        private GameObject _emptyObject;
        public PoolGenerateCommand(ref CD_PoolData poolData, ref Transform poolHolder, ref GameObject emptyObject)
        {
            _poolData = poolData;
            _poolHolder = poolHolder;
            _emptyObject = emptyObject;
            
        }

        public void Execute()
        {
            var poolList = _poolData.Data;

            for (var i = 0; i < poolList.Count; i++)
            {
                _emptyObject = new GameObject();
                _emptyObject.transform.parent = _poolHolder;
                _emptyObject.name = poolList[i].ObjName;

                for (var j = 0; j < poolList[i].ObjectCount; j++)
                {
                    var obj = Object.Instantiate(poolList[i].Prefab, _poolHolder.GetChild(i));
                    obj.SetActive(false);

                }

            }
        }
    }
}