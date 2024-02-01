using System;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class PauseWindowShowing : IInitializable, IDisposable
    {
        private GameObject _pause;
        private UiButton _stop;
        private UiButton _play;

        public PauseWindowShowing(GameObject pause,
                                  UiButton stop,
                                  UiButton play)
        {
            _pause = pause;
            _stop = stop;
            _play = play;
        }

        public void Initialize()
        {
            _stop.Pressed += On;
            _play.Pressed += Off;
        }

        private void On()
            => Show(true);

        private void Off()
            => Show(false);

        private void Show(bool value)
            => _pause.SetActive(value);

        public void Dispose()
        {
            _stop.Pressed -= On;
            _play.Pressed -= Off;
        }
    }
}
