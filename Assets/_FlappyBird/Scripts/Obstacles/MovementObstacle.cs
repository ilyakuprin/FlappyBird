using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class MovementObstacle : IInitializable
    {
        private readonly float _speed;
        private readonly Transform _parent;
        private readonly IMove _iMove;

        private CancellationToken _ct;
        private bool _play;

        public MovementObstacle(float speed,
                                Transform parent,
                                IMove iMove)
        {
            _speed = speed;
            _parent = parent;
            _iMove = iMove;
        }

        public void Initialize()
        {
            _ct = _parent.GetCancellationTokenOnDestroy();
            StartMove();
        }

        public void StartMove()
        {
            _play = true;
            Move().Forget();
            _iMove.StartMove();
        }

        public void StopMove()
        {
            _play = false;
            _iMove.StopMove();
        }

        private async UniTaskVoid Move()
        {
            _parent.position += _speed * Time.deltaTime * Vector3.left;
            await UniTask.NextFrame(_ct);

            if (!_ct.IsCancellationRequested && _play)
            {
                Move().Forget();
            }
        }
    }
}
