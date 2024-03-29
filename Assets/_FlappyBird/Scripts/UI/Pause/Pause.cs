using System;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class Pause : IInitializable, IDisposable
    {
        private readonly UiButton _button;

        public Pause(UiButton button)
        {
            _button = button;
        }

        public void Initialize()
        {
            _button.Pressed += Stop;
        }

        public void Stop()
        {
            Time.timeScale = 0;
        }

        public void Dispose()
        {
            _button.Pressed -= Stop;
        }
    }
}
