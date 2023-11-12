using System;
using RunTime.Enums;
using RunTime.Enums.UIEnums;
using RunTime.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace RunTime.Handler
{
    public class UIEventSubscriber : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIEventSubscriptionTypes subscriptionType;
        [SerializeField] private Button button;

        #endregion

        #region Private Variables

        [ShowInInspector] private UIManager _uiManager;

        #endregion

        #endregion

        private void Awake()
        {
            FindReferences();
        }

        private void FindReferences()
        {
            _uiManager = FindObjectOfType<UIManager>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            switch (subscriptionType)
            {
                case UIEventSubscriptionTypes.OnPlay:
                    button.onClick.AddListener(_uiManager.OnPlay);
                    break;
                case UIEventSubscriptionTypes.OnNextLevel:
                    button.onClick.AddListener(_uiManager.OnNextLevel);
                    break;
                case UIEventSubscriptionTypes.OnRestartLevel:
                    button.onClick.AddListener(_uiManager.OnRestartLevel);
                    break;
                
            }
        }

        private void UnSubscribeEvents()
        {
            switch (subscriptionType)
            {
                case UIEventSubscriptionTypes.OnPlay:
                    button.onClick.RemoveListener(_uiManager.OnPlay);
                    break;
                case UIEventSubscriptionTypes.OnNextLevel:
                    button.onClick.RemoveListener(_uiManager.OnNextLevel);
                    break;
                case UIEventSubscriptionTypes.OnRestartLevel:
                    button.onClick.RemoveListener( _uiManager.OnRestartLevel);
                    break;
                
            }
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}