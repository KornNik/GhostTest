using Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    class EndRaceMenu : BaseUI
    {
        [SerializeField] Button _repeatButton;
        [SerializeField] private LayoutGroup _buttonsGroup;


        private void OnEnable()
        {
            _repeatButton.onClick.AddListener(OnRepeatButtonDown);
        }
        private void OnDisable()
        {
            _repeatButton.onClick.RemoveListener(OnRepeatButtonDown);

        }

        private void OnRepeatButtonDown()
        {
            ChangeGameStateEvent.Trigger(GameStateType.RepeatLevelState);
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            ShowUI.Invoke();

        }
        public override void Hide()
        {
            gameObject.SetActive(false);
            HideUI.Invoke();
        }
    }
}
