using RunTime.Keys;
using RunTime.Signals;
using UnityEngine;

namespace RunTime.Commands.Input
{
    public class InputDraggedOverCommand
    {
        private Vector3 _moveVector;
        public InputDraggedOverCommand(ref Vector3 moveVector)
        {
            _moveVector = moveVector;
        }

        public void Execute()
        {
            InputSignals.Instance.onInputReleased?.Invoke();
            _moveVector = Vector3.zero;
            InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams
            {
                Values = _moveVector
            });
        }
    }
}