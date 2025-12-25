using System;
using Runtime.Enums;
using RunTime.Utilities;
using UnityEngine;

namespace Runtime.Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        public Func<PoolType, GameObject> onGetPoolObject = delegate { return null; };
        public Action<GameObject,PoolType> onSendPoolObject = delegate { };
    }
}