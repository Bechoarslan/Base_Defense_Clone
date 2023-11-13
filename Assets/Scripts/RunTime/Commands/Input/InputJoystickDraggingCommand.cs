using RunTime.Keys;
using RunTime.Signals;
using UnityEngine;

namespace RunTime.Commands.Input
{
    public class InputJoystickDraggingCommand
    {
        private Vector3 _joystickPosition;
        private Vector3 _moveVector;
        public InputJoystickDraggingCommand(ref Vector3 joystickPosition, ref Vector3 moveVector)
        {
            _joystickPosition = joystickPosition;
            _moveVector = moveVector;
        }

        public void Execute(Joystick joysStick)
        {
            _joystickPosition = new Vector3(joysStick.Horizontal,0, joysStick.Vertical);
            _moveVector = _joystickPosition;
            Debug.LogWarning(_moveVector);
            InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams
            {
                Values = _moveVector
            });
        }
    }
}