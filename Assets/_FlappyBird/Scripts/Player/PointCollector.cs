using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Threading;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class PointCollector : IInitializable
    {
        public event Action Added;

        private readonly Rigidbody2D _player;
        private AsyncTriggerEnter2DTrigger _trigger;
        private CancellationToken _ct;

        public PointCollector(Rigidbody2D player)
        {
            _player = player;
        }

        public void Initialize()
        {
            _trigger = _player.gameObject.GetAsyncTriggerEnter2DTrigger();
            _ct = _player.gameObject.GetCancellationTokenOnDestroy();
            StartWait();
        }

        public void StartWait()
            => Wait().Forget();

        private async UniTaskVoid Wait()
        {
            await _trigger.OnTriggerEnter2DAsync(_ct);
            Added?.Invoke();

            if (!_ct.IsCancellationRequested)
                StartWait();
        }
    }
}
