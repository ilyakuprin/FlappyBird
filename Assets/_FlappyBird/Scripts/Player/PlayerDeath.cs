using System;
using Cysharp.Threading.Tasks.Triggers;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class PlayerDeath : IInitializable
    {
        public event Action Died;

        private readonly Rigidbody2D _player;

        private AsyncCollisionEnter2DTrigger _trigger;
        private CancellationToken _ct;

        public PlayerDeath(Rigidbody2D player)
        {
            _player = player;
        }

        public void Initialize()
        {
            _trigger = _player.gameObject.GetAsyncCollisionEnter2DTrigger();
            _ct = _player.gameObject.GetCancellationTokenOnDestroy();
        }

        public void StartWait()
        {
            Wait().Forget();
        }

        private async UniTaskVoid Wait()
        {
            await _trigger.OnCollisionEnter2DAsync(_ct);
            Died?.Invoke();
        }
    }
}
