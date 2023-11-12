using RunTime.Enums;
using RunTime.Extensions;
using UnityEngine.Events;

namespace RunTime.Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<GameStates> onGameStateChanged = delegate { };
        public UnityAction onLevelInitialize = delegate {  };
        public UnityAction onClearActiveLevel = delegate {  };
        public UnityAction onLevelSuccesfull = delegate {  };
        public UnityAction onLevelFailed = delegate {  };
        public UnityAction onNextLevel = delegate {  };
        public UnityAction onRestartLevel = delegate {  };
        public UnityAction onPlay = delegate {  };
        public UnityAction onReset = delegate {  };
        
    }
}