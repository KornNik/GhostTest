using Helpers;
using Controllers;

namespace Behaviours
{
    sealed class PlayerCarLoaderInitializer : IInitialization
    {
        public void Initialization()
        {
            var carLoader = new CarLoader();
            Services.Instance.CarLoader.SetObject(carLoader);
        }
    }
}
