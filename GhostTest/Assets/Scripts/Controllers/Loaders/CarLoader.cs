using Ashsvp;
using Data;
using Helpers;
using UnityEngine;

namespace Controllers
{
    sealed class CarLoader
    {
        private GameObject _car;
        private PlayerCarsBundle _carsBundle;

        public CarLoader()
        {
            _carsBundle = Services.Instance.DataResourcePrefabs.ServicesObject.GetCarsBundle();
        }

        public void LoadCar(Transform spawnPlace)
        {
            var carPrefab = _carsBundle.GetCarByType(CarType.DefaultCar);
            _car = GameObject.Instantiate(carPrefab, spawnPlace.position, spawnPlace.rotation, null);
            if (!ReferenceEquals(_car, null))
            {
                _car.transform.SetPositionAndRotation(spawnPlace.position, spawnPlace.rotation);
                Services.Instance.PlayerVehicleController.SetObject(_car.GetComponent<SimcadeVehicleController>());
            }
        }
        public void ClearCar()
        {
            if(!ReferenceEquals(null, _car))
            {
                GameObject.Destroy(_car.gameObject);
                _car = null;
                Services.Instance.PlayerVehicleController.SetObject(null);
            }
        }

    }
}
