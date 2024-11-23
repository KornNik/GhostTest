using Helpers;

namespace Behaviours
{
    enum RaceCycleEventType
    {
        None,
        Start,
        End
    }
    struct EndRaceEvent
    {
        private static EndRaceEvent _endRaseEvent;

        public static void Trigger()
        {
            EventManager.TriggerEvent(_endRaseEvent);
        }
    }
}
