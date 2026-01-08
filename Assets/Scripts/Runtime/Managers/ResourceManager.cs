using System;
using Runtime.Keys;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class ResourceManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI gemText;

        #endregion

        #region Private Variables

        private ResourcesKeys _resourcesKeys;
        #endregion

        #endregion

        private void Awake()
        {
            _resourcesKeys = new ResourcesKeys(0,0);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GameSignals.Instance.onUpdateCoinText += OnUpdateCoinText;
            GameSignals.Instance.onGetResourceKeys += OnGetResourceKeys;
        }

        private ResourcesKeys OnGetResourceKeys() => _resourcesKeys;
        

        private void OnUpdateCoinText(int value)
        {
            coinText.text = _resourcesKeys.money.ToString();
        }

        private void UnSubscribeEvents()
        {
            GameSignals.Instance.onUpdateGemText -= OnUpdateGemText;
            GameSignals.Instance.onUpdateCoinText -= OnUpdateCoinText;
        }

        private void OnUpdateGemText(int value)
        {
            gemText.text = _resourcesKeys.gem.ToString();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}