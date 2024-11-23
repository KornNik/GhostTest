using Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviours
{
    sealed class Recorder : IRecorder, IEventSubscription, IEventListener<EndRaceEvent>
    {
        private const int MINIMUM_POSITIONS_CHECK_SIZE = 3;
        private const int POSITIONS_SIZE = 100;

        private float _recordDelay = 0.1f;
        private float _recordTime;
        private float _currentDelay;
        private List<Vector3> _positionsList;
        private List<Vector3> _rotationsList;

        private float _cachedRecordTime;
        private List<Vector3> _cachedPositions;
        private List<Vector3> _cachedRotations;

        private bool _isRecording = false;

        public float CachedRecordTime => _cachedRecordTime;
        public List<Vector3> CachedPositions => _cachedPositions;
        public List<Vector3> CachedRotations => _cachedRotations;

        public Recorder()
        {
            _positionsList = new List<Vector3>(POSITIONS_SIZE);
            _rotationsList = new List<Vector3>(POSITIONS_SIZE);
        }

        public void StartRecording()
        {
            _positionsList = new List<Vector3>(POSITIONS_SIZE);
            _rotationsList = new List<Vector3>(POSITIONS_SIZE);
            _recordTime = 0;
            _isRecording = true;
        }
        public void StopRecording()
        {
            _isRecording = false;
            SaveRecord();
        }

        public void Recording(Vector3 position,Quaternion rotation)
        {
            if (_isRecording)
            {
                _recordTime += Time.deltaTime;
                _currentDelay -= Time.deltaTime;
                if (_currentDelay < 0)
                {
                    _positionsList.Add(position);
                    _rotationsList.Add(rotation.eulerAngles);
                    _currentDelay = _recordDelay;
                    return;
                }
            }
        }
        public bool IsHaveSavedValue()
        {
            if (_cachedPositions != null && _cachedRotations != null)
                return true;
            return false;
        }
        public void SaveRecord()
        {
            if (_positionsList.Count > MINIMUM_POSITIONS_CHECK_SIZE)
            {
                _cachedPositions = _positionsList;
                _cachedRotations = _rotationsList;
                _cachedRecordTime = _recordTime;
            }
        }

        public void Subscribe()
        {
            this.EventStartListening<EndRaceEvent>();
        }

        public void Unsubscribe()
        {
            this.EventStopListening<EndRaceEvent>();
        }

        public void OnEventTrigger(EndRaceEvent eventType)
        {
            StopRecording();
        }
    }
}
