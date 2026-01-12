using System;
using Runtime.Enums;
using RunTime.Utilities;
using UnityEngine;

namespace Runtime.Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public Action<PlayerState> onChangePlayerState = delegate { };
        public Action<Transform,PoolType> onSendStacksToHolder = delegate { };
        public Func<Transform> onGetPlayerTransform = delegate { return null; };
        
     
        public Action onStartShootingCoroutine = delegate { };
        
        public Action<GameObject,Transform> onPlayerEnteredBuyArea = delegate { };
        public Action onPlayerExitedBuyArea = delegate { };
        public Action<GameObject> onEnemyDiedClearFromList = delegate { };
        
        public Action onPlayerEnteredSafeZone = delegate { };
        
        public Action<GunType> onChangeGun = delegate { };
        #region Animation Events

        public Action<PlayerAnimState> onTriggerAnimState = delegate { };
        public Action <float,PlayerAnimState> onChangeAnimFloat = delegate { };
        public Action<int,float> onChangeAnimLayer = delegate { };
        public Action<bool,PlayerAnimState> onChangeAnimBool = delegate { };

        public Action<GameObject> onPlayerEnteredMineArea = delegate { };
        #endregion
    }
}