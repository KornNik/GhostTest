﻿using Helpers;

namespace Behaviours
{
    enum GameStateType
    {
        None,
        ManuState,
        ExitGameState,
        GameState,
        EndRaceState,
        LoadGameState,
        RepeatLevelState

    }
    struct ChangeGameStateEvent
    {
        private static ChangeGameStateEvent _changeGameStateEvent;

        private GameStateType _nextGameState;

        public GameStateType NextGameState => _nextGameState;

        public static void Trigger(GameStateType nextGameState)
        {
            _changeGameStateEvent._nextGameState = nextGameState;
            EventManager.TriggerEvent(_changeGameStateEvent);
        }
    }
}
