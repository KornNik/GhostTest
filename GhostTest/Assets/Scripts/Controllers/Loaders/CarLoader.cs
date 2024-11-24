using Ashsvp;
using Data;
using Helpers;
using UnityEngine;

namespace Controllers
{
    sealed class CarLoader : ILoader
    {
        private GameObject _car;
        private PlayerCarsBundle _carsBundle;
        private Transform _spawnTransform;

        public CarLoader()
        {
            _carsBundle = Services.Instance.DataResourcePrefabs.ServicesObject.GetCarsBundle();
        }

        public void LoadCar(Transform spawnTransform)
        {
            _spawnTransform = spawnTransform;
            Load();
        }

        public void Load()
        {
            var carPrefab = _carsBundle.GetCarByType(CarType.DefaultCar);
            _car = GameObject.Instantiate(carPrefab, _spawnTransform.position, _spawnTransform.rotation, null);
            if (!ReferenceEquals(_car, null))
            {
                Services.Instance.PlayerVehicleController.SetObject(_car.GetComponent<SimcadeVehicleController>());
            }
        }
        public void Clear()
        {
            if (!ReferenceEquals(null, _car))
            {
                GameObject.Destroy(_car.gameObject);
                _car = null;
                Services.Instance.PlayerVehicleController.SetObject(null);
            }
        }
    }
}
