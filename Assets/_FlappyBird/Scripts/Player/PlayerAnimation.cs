using System;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class PlayerAnimation : IInitializable, IDisposable
    {
        private readonly Animator _animator;
        private readonly Defeat _defeat;
        private readonly CanvasDefeatBlackout _defeatBlackout;

        public PlayerAnimation(Animator animator,
                               Defeat defeat,
                               CanvasDefeatBlackout defeatBlackout)
        {
            _animator = animator;
            _defeat = defeat;
            _defeatBlackout = defeatBlackout;
        }

        public void Initialize()
        {
            _defeat.Lost += Stop;
            _defeatBlackout.Ended += Play;
        }

        private void Stop()
            => _animator.enabled = false;

        private void Play()
            => _animator.enabled = true;

        public void Dispose()
        {
            _defeat.Lost -= Stop;
            _defeatBlackout.Ended -= Play;
        }
    }
}
