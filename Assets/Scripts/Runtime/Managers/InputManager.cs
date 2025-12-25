using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

   

    #endregion

    #region Private Variables

    private FloatingJoystick _floatingJoystick;

    private InputParamsKeys _inputParamsKeys;
    private bool _isReadyToPlay = true;
    #endregion

    #endregion

    private void Awake()
    {
        _inputParamsKeys = new InputParamsKeys();
        _floatingJoystick = FindObjectOfType<FloatingJoystick>();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        InputSignals.Instance.onInputReadyToPlay += OnInputReadyToPlay;
    }

    private void OnInputReadyToPlay(bool value) => _isReadyToPlay = value;
  

    private void UnSubscribeEvents()
    {
        InputSignals.Instance.onInputReadyToPlay -= OnInputReadyToPlay;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void Update()
    {
        if (!_isReadyToPlay) return;
        _inputParamsKeys.InputParams.x = _floatingJoystick.Horizontal;
        _inputParamsKeys.InputParams.y = _floatingJoystick.Vertical;
        InputSignals.Instance.onInputParamsChanged?.Invoke(_inputParamsKeys);
    }
}