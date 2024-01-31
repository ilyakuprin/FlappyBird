using System;
using UnityEngine.UI;
using Zenject;

namespace FlappyBird
{
    public class GameOverOkButton : IInitializable, IDisposable
    {
        public event Action Pressed;

        private readonly Button _button;

        public GameOverOkButton(Button button)
        {
            _button = button;
        }

        public void Initialize()
            => _button.onClick.AddListener(() => Press());

        private void Press()
            => Pressed?.Invoke();

        public void Dispose()
            => _button.onClick.RemoveListener(() => Press());
    }
}
