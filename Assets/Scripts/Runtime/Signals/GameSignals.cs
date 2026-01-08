using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Keys;
using Runtime.Managers.NPCManager.NPCMoneyCollector;
using RunTime.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class GameSignals : MonoSingleton<GameSignals>
    {
        #region Stack Signals
        
        public Func<Transform,Transform,StackType,float,IEnumerator> onSendBulletStackObjectToHolder = delegate { return null; };
        public Func<Transform,Transform,StackType,IEnumerator> onSendBulletStackObjectToArea = delegate { return null; };
        public Func<Transform> onGetStackAmmoHolderTransform = () => null;
        
        public Action<Transform,GameObject,float> onSendMoneyStackToHolder = delegate { };
        
        public Func<List<GameObject>> onGetMoneyStackList = () => null;
        public Action<GameObject> onAddListDroppedMoneyFromEnemy = delegate { };
        #endregion
        



        #region Turret Signals

        public Action<TurretState> onTurretStateChange = delegate { };
        public Func<(Transform, Transform)> onGetTurretStandPointAndTurretTransform = () => (null, null);
        public Func<Transform> onGetTurretHolderTransform = () => null;
        

        #endregion


        #region Spawn Signals

        public Func<Transform> onEnemyWalkPointTransform = () => null;
        public Func<Transform> onSendNPCMoneyCollectorWalkPoint = () => null;
        
        public UnityAction<NPCMoneyCollectorManager> onSubscribeNPCMoneyCollectorManager = delegate { };

        #endregion

        #region Gem Signals

       
        public Func<(Transform,GemMineType)> onGetMiningAreaTransform = () => (null,GemMineType.CrystalMine);
        public Func<Transform> onGetGemStackAreaTransform = () => null;
        public Action<GameObject> onSendGemToHolder = delegate { };
        public Action<bool> onHostageIsCartMining = delegate { };
        public Action onHostageTakeGemFromCartMine = delegate { };

        #endregion


        #region Resource Signals

        public UnityAction<int> onUpdateCoinText = delegate { };
        public UnityAction<int> onUpdateGemText = delegate { };
        public Func<ResourcesKeys> onGetResourceKeys = () => null;

        #endregion
    }
}