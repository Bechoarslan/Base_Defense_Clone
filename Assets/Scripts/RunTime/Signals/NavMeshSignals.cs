using System;
using RunTime.Extensions;
using UnityEngine;

namespace RunTime.Signals
{
    public class NavMeshSignals : MonoSingleton<NavMeshSignals>
    {
        
        public Func<Vector3> onSendEnemySpawnArea = delegate { return Vector3.zero; };
        public Func<Transform> onSendEnemyWalkArea = delegate { return null; };
    }
}