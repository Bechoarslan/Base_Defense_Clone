using System;
using Cinemachine;
using DG.Tweening;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private CinemachineTransposer transposer;
        #endregion

        #region Private Variables
        private Vector3 _lastCameraPosition;

        #endregion

        #endregion


        private void Awake()
        {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }

        [Button("Change Camera For Turret")]
        public void OnChangeCameraForTurret()
        {
            _lastCameraPosition = transposer.m_FollowOffset;
           var targetOffSet = new Vector3(0, 0.30f, -7f);
            DOTween.To(
                () => transposer.m_FollowOffset,
                x => transposer.m_FollowOffset = x,
                targetOffSet,
                1
            );
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GameSignals.Instance.onChangeCameraForTurret += OnChangeCameraForTurret;
            GameSignals.Instance.onChangeCameraToNormal += OnResetCameraAfterTurret;
        }

        private void UnSubscribeEvents()
        {
            GameSignals.Instance.onChangeCameraForTurret -= OnChangeCameraForTurret;
            GameSignals.Instance.onChangeCameraToNormal -= OnResetCameraAfterTurret;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        [Button("Reset Camera After Turret")]
        public void OnResetCameraAfterTurret()
        {
           
            DOTween.To(
                () => transposer.m_FollowOffset,
                x => transposer.m_FollowOffset = x,
                _lastCameraPosition,
                1
            );
        }

    }
}