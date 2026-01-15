using Runtime.Enums;
using Runtime.Enums.NPCState;
using Runtime.Signals;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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
                case UIEventSubscriberType.PlayerMaxCapacity:
                    button.onClick.AddListener(() =>ChangePlayerProperty(PlayerPropertyType.Capacity));
                    break;
                case UIEventSubscriberType.PlayerMoveSpeed:
                    button.onClick.AddListener(() =>ChangePlayerProperty(PlayerPropertyType.MoveSpeed));
                    break;
                case UIEventSubscriberType.PlayerHealth:
                    button.onClick.AddListener(() =>ChangePlayerProperty(PlayerPropertyType.Health));
                    break;
                case UIEventSubscriberType.NPCMaxCapacity:
                    button.onClick.AddListener( () => ChangeNPCProperty(NPCPropertyType.MaxCapacity));
                    break;
                case UIEventSubscriberType.NPCMoveSpeed:
                    button.onClick.AddListener( () => ChangeNPCProperty(NPCPropertyType.MaxSpeed));
                    break;
                
                
            }
        }

        private void ChangeNPCProperty(NPCPropertyType propertyType)
        {
            GameSignals.Instance.onChangeNPCProperty?.Invoke(propertyType);
        }

        private void ChangePlayerProperty(PlayerPropertyType capacity)
        {
            PlayerSignals.Instance.onChangePlayerPropertyType?.Invoke(capacity);
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
                    button.onClick.RemoveListener(OnClickedCloseUI);
                    break;
                case UIEventSubscriberType.PlayerMaxCapacity:
                    button.onClick.RemoveListener(() =>ChangePlayerProperty(PlayerPropertyType.Capacity));
                    break;
                case UIEventSubscriberType.PlayerMoveSpeed:
                    button.onClick.RemoveListener(() =>ChangePlayerProperty(PlayerPropertyType.MoveSpeed));
                    break;
                case UIEventSubscriberType.PlayerHealth:
                    button.onClick.RemoveListener(() =>ChangePlayerProperty(PlayerPropertyType.Health));
                    break;
                
            }
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}