using System;
using RunTime.Commands.Input;
using RunTime.Data.UnityObject;
using RunTime.Data.ValueObject;
using RunTime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RunTime.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables
        public Vector3? MousePosition;
        #region Private Variables

        [ShowInInspector] private bool _isAvailableForTouch;
        [ShowInInspector] private DynamicJoystick _joysStick;
        [ShowInInspector] private bool _isFirstTimeTouchTaken;
        

        #endregion

        [ShowInInspector] private InputData _inputData;
       
        private Vector3 _joystickPosition;
        private bool _isTouching;
        private float _currentVelocity;
        private Vector3 _moveVector;
        private InputDraggedOverCommand _inputDraggedOverCommand;
        private InputStartedDraggedCommand _inputStartedDraggedCommand;
        private InputJoystickDraggingCommand _inputJoystickDraggingCommand;
        private readonly string _inputDataPath = "Data/CD_InputData";
        #endregion

        private void Awake()
        {
            _inputData = GetInputData();
            Init();
        }

        private void Init()
        {
            _inputDraggedOverCommand = new InputDraggedOverCommand(ref _moveVector);
            _inputStartedDraggedCommand = new InputStartedDraggedCommand(ref _isFirstTimeTouchTaken, this);
            _inputJoystickDraggingCommand =
                new InputJoystickDraggingCommand(ref _joystickPosition, ref _moveVector);
        }
        private InputData GetInputData() =>  Resources.Load<CD_InputData>(_inputDataPath).Data;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;

        }

        private void OnPlay()
        {
            _joysStick = FindObjectOfType<DynamicJoystick>();
            _isAvailableForTouch = true;
        }

        private void OnReset()
        {
            _isTouching = false;
            _isFirstTimeTouchTaken = false;
            _isAvailableForTouch = false;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Update()
        {
            if (!_isAvailableForTouch) return;
            if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;
                _inputDraggedOverCommand.Execute();
            }

            if (Input.GetMouseButtonDown(0))
            {
                
                _isTouching = true;
                _inputStartedDraggedCommand.Execute();
            }

            if (Input.GetMouseButton(0))
            {
                if (_isTouching)
                {
                    
                    
                    _inputJoystickDraggingCommand.Execute(_joysStick);
                }
                
            }
        }
    }
}