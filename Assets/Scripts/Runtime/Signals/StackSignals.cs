using System;
using System.Collections;
using Runtime.Enums;
using RunTime.Utilities;
using UnityEngine;

namespace Runtime.Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        #region Stack Signals
        
        public Func<Transform,Transform,StackType,IEnumerator> onSendStackObjectToHolder = delegate { return null; };
        public Func<Transform,Transform,StackType,IEnumerator> onSendStackObjectToArea = delegate { return null; };
        public Func<Transform> onGetAmmoStackHolderTransform = () => null;
        #endregion
        



        #region Turret Signals

        public Func<(Transform, Transform)> onGetTurretStandPointAndTurretTransform = () => (null, null);
        public Func<Transform> onGetTurretHolderTransform = () => null;
        

        #endregion
    }
}