using System;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class ActivationPlayer : IInitializable, IDisposable
    {
        private readonly Rigidbody2D _player;
        private readonly CanvasDefeatBlackout _defeat;
        private readonly CanvasMenuBlackoutRemoval _menu;

        public ActivationPlayer(Rigidbody2D player,
                                CanvasDefeatBlackout defeat,
                                CanvasMenuBlackoutRemoval menu)
        {
            _player = player;
            _defeat = defeat;
            _menu = menu;
        }

        public void Initialize()
        {
            Off();
            
            _defeat.Ended += Off;
            _menu.Ended += On;
        }

        private void Off()
            => SetActive(false);

        private void On()
            => SetActive(true);

        private void SetActive(bool value)
            => _player.gameObject.SetActive(value);

        public void Dispose()
        {
            _defeat.Ended -= Off;
            _menu.Ended -= On;
        }
    }
}