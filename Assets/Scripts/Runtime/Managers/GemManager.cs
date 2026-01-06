using System.Collections.Generic;
using Runtime.Enums.NPCState;
using Runtime.Managers.NPCManager.Hostage;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class GemManager : MonoBehaviour
    {

        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> gemWorkerList;
        [SerializeField] private List<Transform> gemPosList;

        #endregion

        #region Private Variables

        #endregion

        #endregion




        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GameSignals.Instance.onHostageEnteredGemHouse += OnHostageEnteredGemHouse;
            GameSignals.Instance.onGetMiningAreaTransform += OnGetMiningAreaTransform;
        }

        private Transform OnGetMiningAreaTransform()
        {
            var randomIndex = Random.Range(0, gemPosList.Count);
            return gemPosList[randomIndex];
        }

        private void OnHostageEnteredGemHouse(GameObject obj)
        {
            if (gemWorkerList.Contains(obj)) return;
            gemWorkerList.Add(obj);
            var hostageManager = obj.GetComponent<NPCHostageManager>();
            hostageManager.SwitchState(NPCHostageStateType.Mine);
        }

        private void UnSubscribeEvents()
        {
            GameSignals.Instance.onHostageEnteredGemHouse += OnHostageEnteredGemHouse;
            GameSignals.Instance.onGetMiningAreaTransform += OnGetMiningAreaTransform;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}