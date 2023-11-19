using System;
using System.Collections.Generic;
using RunTime.Enums.Pool;
using RunTime.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace RunTime.Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        public Func<PoolType,GameObject> onGetPoolObject = delegate { return null; };
        public UnityAction<GameObject,PoolType> onSendPool = delegate {  };
        
    }
}