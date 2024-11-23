using Helpers;
using System;

namespace Behaviours
{
    class GameStateController : BaseStateController, IEventListener<ChangeGameStateEvent>,
        IEventSubscription, IDisposable
    {
        private IState _menuState;
        private IState _gameState;
        private IState _loadingGameState;
        private IState _endRaceState;
        private IState _exitGameState;
        private IState _repeatState;

        public GameStateController()
        {
            InitializeStates();
            StartState(MenuState);
            Subscribe();
        }
        public void Dispose()
        {
            Unsubscribe();
        }

        public IState MenuState => _menuState;
        public IState GameState => _gameState;
        public IState LoadingGameState => _loadingGameState;
        public IState EndRaceState => _endRaceState;
        public IState ExitGameState => _exitGameState;
        public IState RepeatState => _repeatState;

        protected override void InitializeStates()
        {
            _menuState = new MenuState(this);
            _loadingGameState = new LoadGameState(this);
            _gameState = new GameState(this);
            _endRaceState = new EndRaceState(this);
            _exitGameState = new ExitGameState(this);
            _repeatState = new RepeatLevelState(this);
        }


        public void OnEventTrigger(ChangeGameStateEvent eventType)
        {
            switch (eventType.NextGameState)
            {
                case GameStateType.None:
                    throw new System.Exception("State is unknown");
                case GameStateType.ManuState:
                    ChangeState(_menuState);
                    break;
                case GameStateType.GameState:
                    ChangeState(_gameState);
                    break;
                case GameStateType.LoadGameState:
                    ChangeState(_loadingGameState);
                    break;
                case GameStateType.ExitGameState:
                    ChangeState(_exitGameState);
                    break;
                case GameStateType.EndRaceState:
                    ChangeState(_endRaceState);
                    break;
                case GameStateType.RepeatLevelState:
                    ChangeState(_repeatState);
                    break;
            }
        }

        public void Subscribe()
        {
            this.EventStartListening<ChangeGameStateEvent>();
        }

        public void Unsubscribe()
        {
            this.EventStopListening<ChangeGameStateEvent>();
        }
    }
}