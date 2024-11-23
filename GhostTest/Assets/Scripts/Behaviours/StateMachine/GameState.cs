using UI;
using UnityEngine;
using Cinemachine;
using Ashsvp;
using Controllers;
using Helpers;

namespace Behaviours
{
    class GameState : BaseState
    {
        private Level _level;
        private Camera _camera;
        private Recorder _recorder;
        private EndLevel _endLevel;
        private GhostProjector _projector;
        private SimcadeVehicleController _currentCar;
        private CinemachineVirtualCamera _cinemachine;
        private BaseCarInputs _carInputs;

        public GameState(GameStateController stateController) : base(stateController)
        {
            _carInputs = new BaseCarInputs();
            _endLevel = new EndLevel();
            _camera = Services.Instance.CameraService.ServicesObject;
            _cinemachine = _camera.GetComponent<CinemachineVirtualCamera>();
            _recorder = new Recorder();
        }

        public override void EnterState()
        {
            _endLevel.Subscribe();
            _recorder.Subscribe();
            ScreenInterface.GetInstance().Execute(ScreenTypes.GameMenu);
            _recorder.StartRecording();
            _level = Services.Instance.Level.ServicesObject;
            _currentCar = Services.Instance.PlayerVehicleController.ServicesObject;
            _cinemachine.Follow = _currentCar.transform;
            _cinemachine.LookAt = _currentCar.transform;
            SetDefaultCarPosition();
            _carInputs.SetCar(_currentCar);
            _projector = new GhostProjector(_currentCar.VehicleBody.gameObject);

            if (_recorder.IsHaveSavedValue())
            {
                _projector.StartProjectCar(_recorder.CachedRecordTime, _recorder.CachedPositions,_recorder.CachedRotations);
            }
        }

        public override void ExitState()
        {
            _recorder.Unsubscribe();
            _endLevel.Unsubscribe();
            _currentCar = null;
            _level = null;
            _cinemachine.Follow = null;
            _cinemachine.LookAt = null;
        }
        public override void LogicFixedUpdate()
        {
        }
        public override void LogicUpdate()
        {
            HandleInput();
            _recorder.Recording(_currentCar.transform.position, _currentCar.transform.rotation);
        }
        public void HandleInput()
        {
            _carInputs.UpdateControll();
        }
        private void SetDefaultCarPosition()
        {
            _currentCar.localVehicleVelocity = Vector3.zero;
            _currentCar.gameObject.transform.position = _level.GetPlayerSpawnPlace().position;
            _currentCar.gameObject.transform.rotation = _level.GetPlayerSpawnPlace().rotation;
        }
    }
}
