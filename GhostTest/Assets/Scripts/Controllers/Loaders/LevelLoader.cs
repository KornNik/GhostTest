using UnityEngine;
using Data;
using Helpers;
using Behaviours;

namespace Controllers
{
    sealed class LevelLoader : ILoader
    {
        private GameObject _level;
        private LevelData _levelData;
        private int _index;

        public void LoadLevelGame(int index)
        {
            _index = index;
            Load();
        }

        public void Load()
        {
            _levelData = Services.Instance.DatasBundle.ServicesObject.GetData<LevelsBundle>().GetRandomLevelData();
            _level = GameObject.Instantiate(_levelData.GetPrefab(), _levelData.GetLevelPosition(), Quaternion.identity);
            if (!ReferenceEquals(_level, null))
            {
                Services.Instance.Level.SetObject(_level.GetComponent<Level>());
            } 
        }
        public void Clear()
        {
            if (!ReferenceEquals(_level, null))
            {
                GameObject.Destroy(_level.gameObject);
                _level = null;
                Services.Instance.Level.SetObject(null);
            }
        }
    }
}
