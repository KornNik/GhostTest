namespace Behaviours
{
    sealed class RepeatLevelState : LoadGameState
    {
        public RepeatLevelState(GameStateController stateController) : base(stateController)
        {
        }

        public override void EnterState()
        {
            ClearLevel();
            LoadLevel();
            LoadPlayerCar();
            ChangeGameStateEvent.Trigger(GameStateType.GameState);
        }
    }
}
