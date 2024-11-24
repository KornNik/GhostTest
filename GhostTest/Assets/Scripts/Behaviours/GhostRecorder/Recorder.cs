using System.Collections.Generic;
using UnityEngine;

namespace Behaviours
{
    sealed class Recorder : IRecorder
    {
        private const int MINIMUM_POSITIONS_CHECK_SIZE = 3;
        private const int POSITIONS_SIZE = 100;

        private float _recordDelay = 0.1f;
        private float _recordTime;
        private float _currentDelay;
        private List<Vector3> _positionsList;
        private List<Vector3> _rotationsList;
        private Transform _recordingTransform;

        private float _cachedRecordTime;
        private List<Vector3> _cachedPositions;
        private List<Vector3> _cachedRotations;

        public float CachedRecordTime => _cachedRecordTime;
        public List<Vector3> CachedPositions => _cachedPositions;
        public List<Vector3> CachedRotations => _cachedRotations;

        public Recorder()
        {
            _positionsList = new List<Vector3>(POSITIONS_SIZE);
            _rotationsList = new List<Vector3>(POSITIONS_SIZE);
        }

        public void StartRecording(Transform recordingTransform)
        {
            _positionsList = new List<Vector3>(POSITIONS_SIZE);
            _rotationsList = new List<Vector3>(POSITIONS_SIZE);
            _recordingTransform = recordingTransform;
            _recordTime = 0;
        }
        public void StopRecording()
        {
            AddPositions();
            SaveRecord();
        }

        public void Recording()
        {
            _recordTime += Time.deltaTime;
            _currentDelay -= Time.deltaTime;
            if (_currentDelay < 0)
            {
                AddPositions();
                _currentDelay = _recordDelay;
                return;
            }
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
        public bool IsHaveSavedValue()
        {
            if (_cachedPositions != null && _cachedRotations != null)
                return true;
            return false;
        }
        private void AddPositions()
        {
            _positionsList.Add(_recordingTransform.position);
            _rotationsList.Add(_recordingTransform.rotation.eulerAngles);
        }
    }
}
