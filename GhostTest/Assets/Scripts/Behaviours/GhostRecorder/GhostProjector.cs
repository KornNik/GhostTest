using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace Behaviours
{
    sealed class GhostProjector
    {
        private GameObject _carGhost;
        private float _cachedRecordTime;
        private List<Vector3> _cachedPositions;
        private List<Vector3> _cachedRotations;

        private float _tikPerPositionsValue;
        private int _tiksInMilliseconds;
        private UniTask _projectorTask;

        private int _index;

        public void SetModel(GameObject gameObject)
        {
            _carGhost = GameObject.Instantiate(gameObject);
        }
        public void StartProjectCar(float recordTime, List<Vector3> cachedPositions, List<Vector3> cachedRotations)
        {
            _cachedPositions = cachedPositions;
            _cachedRotations = cachedRotations;
            _cachedRecordTime = recordTime;
            _tikPerPositionsValue = _cachedRecordTime / _cachedPositions.Count;
            _tiksInMilliseconds = Mathf.RoundToInt(_tikPerPositionsValue * 1000);
            _projectorTask = ProjectorTask();
        }
        public bool IsHaveModel()
        {
            if(_carGhost != null) 
                return true;
            return false;
        }
        private async UniTask ProjectorTask()
        {
            _carGhost.transform.position = _cachedPositions[0];
            _carGhost.transform.rotation = Quaternion.Euler(_cachedRotations[0]);
            while (_index < _cachedPositions.Count)
            {
                _carGhost.transform.DOMove(_cachedPositions[_index], _tikPerPositionsValue).SetEase(Ease.Linear);
                _carGhost.transform.DORotate(_cachedRotations[_index], _tikPerPositionsValue).SetEase(Ease.Linear);
                await UniTask.Delay(_tiksInMilliseconds);
                _index++;
            }
            _index = 0;
        }
        private void SetPosition(Vector3 position)
        {
            _carGhost.transform.position = position;
        }
        private void SetRotation(Quaternion rotation)
        {
            _carGhost.transform.rotation = rotation;
        }
    }
}
