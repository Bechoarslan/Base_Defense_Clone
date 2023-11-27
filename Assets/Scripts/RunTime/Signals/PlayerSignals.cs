using RunTime.Enums;
using RunTime.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace RunTime.Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<bool> onPlayConditionChanged = delegate {  };
        public UnityAction<bool> onMoveConditionChanged = delegate {  };
        
        public UnityAction<PlayerAnimationState> onSetPlayerAnimationState = delegate {  };
        
        public UnityAction<GameObject> onPLayerInteractWithTurret = delegate {  };
        public UnityAction onPlayerExitInteractWithTurret = delegate {  };
        
        public UnityAction<Transform> onPlayerInteractWithBulletArea = delegate { };
        public UnityAction<Transform> onPlayerInteractWithTurretBulletArea = delegate {  };
        
    }
}