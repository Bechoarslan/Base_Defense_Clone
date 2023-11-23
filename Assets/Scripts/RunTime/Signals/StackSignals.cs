using RunTime.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace RunTime.Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<Transform> onPlayerInteractWithBulletArea = delegate { };
        public UnityAction<Transform> onPlayerInteractWithTurretBulletArea = delegate {  };
    }
}