using System.Collections.Generic;
using Runtime.Enums.NPCState;
using Runtime.Managers.NPCManager.Hostage;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class GemManager : MonoBehaviour
    {

        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> gemWorkerList;
        [SerializeField] private List<Transform> gemPosList;
        [SerializeField] private Transform gemHolderTransform;

        #endregion

        #region Private Variables

        private List<GameObject> _gemObjList = new List<GameObject>();
        #endregion

        #endregion




        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerEnteredMineArea += OnPlayerEnteredMineArea;
            GameSignals.Instance.onGetMiningAreaTransform += OnGetMiningAreaTransform;
            GameSignals.Instance.onGetGemStackAreaTransform += OnSendGemStackAreaTransform;
            GameSignals.Instance.onSendGemToHolder += OnSendGemToHolder;
        }

        private Transform OnSendGemStackAreaTransform() => gemHolderTransform;
        

        private void OnPlayerEnteredMineArea(GameObject obj)
        {
            gemWorkerList.Add(obj);
            var objective = obj.GetComponent<NPCHostageManager>();
            if (objective == null) return;
            objective.SwitchState(NPCHostageStateType.Mine);

        }

        [Button("add gem to holder")]
        private void OnSendGemToHolder(GameObject gemObj)
        {

            _gemObjList.Add(gemObj);

            int index = _gemObjList.Count - 1;

            int xIndex = index % 6;
            int zIndex = (index / 6) % 6;
            int yIndex = index / 36; // 6x6 = 36, katman mantığı

            float spacing = 0.3f;

            gemObj.transform.SetParent(gemHolderTransform);
            gemObj.transform.localPosition = new Vector3(
                xIndex * spacing,
                yIndex * spacing,
                zIndex * spacing
            );
            gemObj.transform.localRotation = Quaternion.identity;
            
                
            
        }
        private Transform OnGetMiningAreaTransform()
        {
            var randomIndex = Random.Range(0, gemPosList.Count);
            return gemPosList[randomIndex];
        }

       
        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerEnteredMineArea -= OnPlayerEnteredMineArea;
            GameSignals.Instance.onGetMiningAreaTransform -= OnGetMiningAreaTransform;
            GameSignals.Instance.onGetGemStackAreaTransform -= OnSendGemStackAreaTransform;
            GameSignals.Instance.onSendGemToHolder -= OnSendGemToHolder;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}