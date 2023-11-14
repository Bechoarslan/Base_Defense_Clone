using RunTime.Extensions;
using UnityEngine.Events;

namespace RunTime.Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<bool> onPlayConditionChanged = delegate {  };
        public UnityAction<bool> onMoveConditionChanged = delegate {  };
        
    }
}