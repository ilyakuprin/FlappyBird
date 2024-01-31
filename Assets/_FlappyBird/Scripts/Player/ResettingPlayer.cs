using System;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class ResettingPlayer : IInitializable, IDisposable
    {
        private readonly Transform _player;
        private readonly CanvasDefeatBlackout _defeat;
        private Vector3 _startPosition;

        public ResettingPlayer(Rigidbody2D player,
                               CanvasDefeatBlackout defeat)
        {
            _player = player.transform;
            _defeat = defeat;
        }

        public void Initialize()
        {
            _startPosition = _player.position;
            _defeat.Ended += Reset;
        }

        private void Reset()
            => _player.position = _startPosition;

        public void Dispose()
            => _defeat.Ended -= Reset;
    }
}
