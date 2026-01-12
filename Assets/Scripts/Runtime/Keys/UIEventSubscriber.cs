using Runtime.Enums;
using Runtime.Signals;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Keys
{
    public class UIEventSubscriber : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Button button;
        [SerializeField] private UIEventSubscriberType subscriberType;
        [SerializeField] private UIType uiType;

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
            switch (subscriberType)
            {
                case UIEventSubscriberType.ShotgunPressed:
                    button.onClick.AddListener(() => ChangeGunType(GunType.Shotgun));
                    break;
                case UIEventSubscriberType.PistolPressed:
                    button.onClick.AddListener(() => ChangeGunType(GunType.Pistol));
                    break;
                case UIEventSubscriberType.RiflePressed:
                    button.onClick.AddListener(() => ChangeGunType(GunType.Rifle));
                    break;
                case UIEventSubscriberType.CloseUI:
                    button.onClick.AddListener(OnClickedCloseUI);
                    break;
                
                
            }
        }

        private void OnClickedCloseUI()
        {
            InputSignals.Instance.onInputReadyToPlay?.Invoke(true);
            UISignals.Instance.onCloseUIPanel?.Invoke(uiType);
        }

        private void ChangeGunType(GunType gunType)
        {
            PlayerSignals.Instance.onChangeGun?.Invoke(gunType);
            
        }

        private void UnSubscribeEvents()
        {
            switch (subscriberType)
            {
                case UIEventSubscriberType.ShotgunPressed:
                    button.onClick.RemoveListener(() => ChangeGunType(GunType.Shotgun));
                    break;
                case UIEventSubscriberType.PistolPressed:
                    button.onClick.RemoveListener(() => ChangeGunType(GunType.Pistol));
                    break;
                case UIEventSubscriberType.RiflePressed:
                    button.onClick.RemoveListener(() => ChangeGunType(GunType.Rifle));
                    break;
                case UIEventSubscriberType.CloseUI:
                    break;
                
            }
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}