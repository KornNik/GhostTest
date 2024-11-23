using Helpers.Extensions;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CarsBundle", menuName = "Data/CarsBundle")]
    sealed class PlayerCarsBundle : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<CarType, GameObject> _cars;

        public GameObject GetCarByType(CarType carType)
        {
            GameObject carPrefab = default;
            if (_cars.Contains(carType))
            {
                carPrefab = _cars[carType];
            }
            return carPrefab;
        }
    }
    enum CarType
    {
        None,
        DefaultCar
    }
}
