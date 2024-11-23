using UI;

namespace Behaviours
{
    sealed class EndRaceState : BaseState
    {
        public EndRaceState(GameStateController stateController) : base(stateController)
        {

        }

        public override void EnterState()
        {
            ScreenInterface.GetInstance().Execute(Helpers.ScreenTypes.EndRaceMenu);
        }

        public override void ExitState()
        {
        }

        public override void LogicFixedUpdate()
        {
        }

        public override void LogicUpdate()
        {
        }
    }
}