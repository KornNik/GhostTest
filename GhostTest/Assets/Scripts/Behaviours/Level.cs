using UnityEngine;

namespace Behaviours
{
    sealed class Level : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPlace;

        public Transform GetPlayerSpawnPlace()
        {
            return _playerSpawnPlace;
        }
    }
}
