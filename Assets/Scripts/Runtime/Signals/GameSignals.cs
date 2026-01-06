using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Enums;
using RunTime.Utilities;
using UnityEngine;

namespace Runtime.Signals
{
    public class GameSignals : MonoSingleton<GameSignals>
    {
        #region Stack Signals
        
        public Func<Transform,Transform,StackType,IEnumerator> onSendStackObjectToHolder = delegate { return null; };
        public Func<Transform,Transform,StackType,IEnumerator> onSendStackObjectToArea = delegate { return null; };
        public Func<Transform> onGetStackAmmoHolderTransform = () => null;
        #endregion
        



        #region Turret Signals

        public Action<TurretState> onTurretStateChange = delegate { };
        public Func<(Transform, Transform)> onGetTurretStandPointAndTurretTransform = () => (null, null);
        public Func<Transform> onGetTurretHolderTransform = () => null;
        

        #endregion


        #region Spawn Signals

        public Func<Transform> onEnemyWalkPointTransform = () => null;

        #endregion

        #region Gem Signals

       
        public Func<Transform> onGetMiningAreaTransform = () => null;
        public Func<Transform> onGetGemStackAreaTransform = () => null;
        public Action<GameObject> onSendGemToHolder = delegate { };

        #endregion
    }
}