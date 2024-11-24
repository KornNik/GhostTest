namespace Behaviours
{
    sealed class ExitGameState : LoadGameState
    {
        public ExitGameState(GameStateController stateController) : base(stateController)
        {
        }

        public override void EnterState()
        {
            ClearLevel();
            ChangeGameStateEvent.Trigger(GameStateType.ManuState);
        }
    }
}
