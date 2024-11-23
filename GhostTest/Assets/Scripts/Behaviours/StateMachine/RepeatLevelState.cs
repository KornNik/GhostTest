namespace Behaviours
{
    sealed class RepeatLevelState : LoadGameState
    {
        public RepeatLevelState(GameStateController stateController) : base(stateController)
        {
        }

        public override void EnterState()
        {
            ClearLevelOnExit();
            LoadLevel();
            LoadPlayerCar();
            ChangeGameStateEvent.Trigger(GameStateType.GameState);
        }
    }
}
