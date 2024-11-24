using UI;
using UnityEngine;
using Cinemachine;
using Ashsvp;
using Controllers;
using Helpers;
using System.Collections.Generic;

namespace Behaviours
{
    class GameState : BaseState
    {
        private Level _level;
        private Camera _camera;
        private Recorder _recorder;
        private EndLevel _endLevel;
        private BaseCarInputs _carInputs;
        private GhostProjector _projector;
        private SimcadeVehicleController _currentCar;
        private CinemachineVirtualCamera _cinemachine;

        private List<IEventSubscription> _subscriptions = new List<IEventSubscription>();

        public GameState(GameStateController stateController) : base(stateController)
        {
            _carInputs = new BaseCarInputs();
            _endLevel = new EndLevel();
            _camera = Services.Instance.CameraService.ServicesObject;
            _cinemachine = _camera.GetComponent<CinemachineVirtualCamera>();
            _recorder = new Recorder();
            _projector = new GhostProjector();

            FillSubscriptions();
        }

        public override void EnterState()
        {
            base.EnterState();

            AddSubsriptions();

            ScreenInterface.GetInstance().Execute(ScreenTypes.GameMenu);

            _level = Services.Instance.Level.ServicesObject;
            _currentCar = Services.Instance.PlayerVehicleController.ServicesObject;

            _cinemachine.Follow = _currentCar.transform;
            _cinemachine.LookAt = _currentCar.transform;
            _carInputs.SetCar(_currentCar);

            GhostRecorderStart();
        }
        public override void ExitState()
        {
            base.ExitState();

            RemoveSubsriptions();
            _recorder.StopRecording();
            _currentCar = null;
            _level = null;
            _cinemachine.Follow = null;
            _cinemachine.LookAt = null;
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            HandleInput();
            _recorder.Recording();
        }
        public void HandleInput()
        {
            _carInputs.UpdateControll();
        }

        private void GhostRecorderStart()
        {
            _recorder.StartRecording(_currentCar.transform);
            if (_recorder.IsHaveSavedValue())
            {
                if (!_projector.IsHaveModel()) { _projector.SetModel(_currentCar.VehicleBody.gameObject); }
                _projector.StartProjectCar(_recorder.CachedRecordTime, _recorder.CachedPositions, _recorder.CachedRotations);
            }
        }


        #region Subscriptions

        private void AddSubsriptions()
        {
            foreach (var item in _subscriptions)
            {
                item.Subscribe();
            }
        }
        private void RemoveSubsriptions()
        {
            foreach (var item in _subscriptions)
            {
                item.Unsubscribe();
            }
        }
        private void FillSubscriptions()
        {
            _subscriptions.Add(_endLevel);
        }

        #endregion

    }
}
