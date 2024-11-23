using Controllers;
using Helpers;

namespace Behaviours
{
    class LoadGameState : BaseState
    {
        private CarLoader _carLoader;
        private LevelLoader _levelLoader;

        public LoadGameState(GameStateController stateController) : base(stateController)
        {
            _carLoader = Services.Instance.CarLoader.ServicesObject;
            _levelLoader = Services.Instance.LevelLoader.ServicesObject;
        }
        public override void EnterState()
        {
            base.EnterState();
            LoadLevel();
            LoadPlayerCar();
            ChangeGameStateEvent.Trigger(GameStateType.GameState);
        }
        protected void LoadLevel()
        {
            _levelLoader.LoadLevelGame(0);
        }
        protected void LoadPlayerCar()
        {
            _carLoader.LoadCar(Services.Instance.Level.ServicesObject.GetPlayerSpawnPlace());
        }
        protected void ClearLevelOnExit()
        {
            _carLoader.ClearCar();
            _levelLoader.ClearLevelFull();
        }
    }
}
