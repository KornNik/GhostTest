using Helpers;

namespace Behaviours
{
    sealed class EndLevel : IEventSubscription, IEventListener<EndRaceEvent>
    {
        public void Subscribe()
        {
            this.EventStartListening<EndRaceEvent>();
        }

        public void Unsubscribe()
        {
            this.EventStopListening<EndRaceEvent>();
        }

        public void OnEventTrigger(EndRaceEvent eventType)
        {
            ChangeGameStateEvent.Trigger(GameStateType.EndRaceState);
        }
    }
}
