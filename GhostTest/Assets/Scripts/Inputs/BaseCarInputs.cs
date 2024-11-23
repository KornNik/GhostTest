using Ashsvp;
using Helpers;
using UnityEngine;

namespace Controllers
{
    sealed class BaseCarInputs : BaseInputs<SimcadeVehicleController>
    {
        private SimcadeVehicleController _controlledCar;
        private InputController _inputController;
        public BaseCarInputs(SimcadeVehicleController controllObject = null) : base(controllObject)
        {
            _controlledCar = controllObject;
            _inputController = Services.Instance.InputController.ServicesObject;
        }

        public void SetCar(SimcadeVehicleController controllObject)
        {
            _controlledCar = controllObject;
        }

        public override void UpdateControll()
        {
            var movementInput = _inputController.InputActions.
                PlayerActionList[InputActionManagerPlayer.MOVEMENT];
            var handBreakInput = _inputController.InputActions.
                PlayerActionList[InputActionManagerPlayer.JUMP];
            var movementInputs = _inputController.InputActions.PlayerActionList
                [InputActionManagerPlayer.MOVEMENT].ReadValue<Vector2>();

            if (_controlledCar.CanDrive && _controlledCar.CanAccelerate)
            {
                _controlledCar.accelerationInput = movementInputs.y;
                _controlledCar.steerInput = movementInputs.x;

                _controlledCar.brakeInput = handBreakInput.ReadValue<float>();
            }
            else if (_controlledCar.CanDrive && !_controlledCar.CanAccelerate)
            {
                _controlledCar.accelerationInput = 0;
                _controlledCar.steerInput = movementInputs.x;

                _controlledCar.brakeInput = handBreakInput.ReadValue<float>();
            }
            else
            {
                StopCar();
            }
        }
        private void StopCar()
        {
            _controlledCar.accelerationInput = 0;
            _controlledCar.steerInput = 0;
            _controlledCar.brakeInput = 1;
        }
    }
}
