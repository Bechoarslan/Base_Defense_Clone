using Runtime.Controllers.UI;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIController uiController;

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
            UISignals.Instance.onOpenUIPanel += uiController.OpenPanel;
            UISignals.Instance.onCloseAllPanels += uiController.CloseAllPanels;
            UISignals.Instance.onCloseUIPanel += uiController.CloseUIPanel;
          
        }

        private void UnSubscribeEvents()
        {
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}