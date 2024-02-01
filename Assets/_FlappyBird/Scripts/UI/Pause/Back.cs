using System;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class Back : IInitializable, IDisposable
    {
        private readonly UiButton _button;

        public Back(UiButton button)
        {
            _button = button;
        }

        public void Initialize()
        {
            _button.Pressed += Play;
        }

        public void Play()
        {
            Time.timeScale = 1;
        }

        public void Dispose()
        {
            _button.Pressed -= Play;
        }
    }
}
