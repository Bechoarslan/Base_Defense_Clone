using System;
using Cinemachine;
using RunTime.Enums.Camera;
using RunTime.Signals;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using CameraState = UnityEditor.Rendering.LookDev.CameraState;

namespace RunTime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CinemachineStateDrivenCamera _stateDrivenCamera;
        [SerializeField] private Animator cameraAnimator;

        #endregion

        #region Private Variables

        private float3 _initialPosition;
        

        #endregion

        #endregion


        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _initialPosition = transform.position;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            CameraSignals.Instance.onChangeCameraState += OnChangeCameraState;
            CameraSignals.Instance.onSetCinemachineTarget += OnSetCinemachineTarget;
        }

        private void OnSetCinemachineTarget()
        {
            var target = GameObject.FindObjectOfType<PlayerManager>().transform;
            _stateDrivenCamera.Follow = target;
        }

        private void OnChangeCameraState(CameraEnums cameraState)
        {
            cameraAnimator.SetTrigger(cameraState.ToString());
        }


        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            CameraSignals.Instance.onChangeCameraState -= OnChangeCameraState;
            CameraSignals.Instance.onSetCinemachineTarget -= OnSetCinemachineTarget;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        private void OnReset()
        {
            transform.position = _initialPosition;
        }
    }
}