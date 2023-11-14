using RunTime.Data.UnityObject;
using UnityEngine;

namespace RunTime.Commands.Pool
{
    public class PoolResetCommand
    {
        private CD_PoolData _poolData;
        private Transform _poolHolder;
        private GameObject _levelHolder;
        public PoolResetCommand(ref CD_PoolData poolData, ref Transform poolHolder, ref GameObject levelHolder)
        {
            _poolData = poolData;
            _poolHolder = poolHolder;
            _levelHolder = levelHolder;
        }

        public void Execute()
        {
            var poolList = _poolData.Data;

            for (var i = 0; i < poolList.Count; i++)
            {
                var child = _poolHolder.GetChild(i);
                if (child.transform.childCount > poolList[i].ObjectCount)
                {
                    var count = child.transform.childCount;
                    for (var j = poolList[i].ObjectCount; j < count; j++)
                    {
                        child.GetChild(poolList[i].ObjectCount).SetParent(_levelHolder.transform.GetChild(0));
                        
                    }
                }

                {

                }
            }
        }

            }
        }
    
