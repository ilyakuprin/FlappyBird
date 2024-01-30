using System;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class PlayerBouncing : IInitializable, IDisposable
    {
        private readonly PlayerInput _input;
        private readonly Rigidbody2D _player;
        private readonly float _force;

        public PlayerBouncing(PlayerInput input,
                              Rigidbody2D player,
                              PlayerConfig config)
        {
            _input = input;
            _player = player;
            _force = config.ForceBounce;
        }

        public void Initialize()
            => _input.Inputted += Bounce;

        private void Bounce(InputData inputData)
        {
            if (inputData.Jump)
                _player.velocity = new Vector2(_player.velocity.x, _force);
        }

        public void Dispose()
            => _input.Inputted -= Bounce;
    }
}
