using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class MovementEarth : IInitializable, IMove
    {
        private readonly Camera _camera;
        private readonly Transform[] _earths;

        private CancellationToken[] _ct;
        private int _leftmostIndex;
        private bool _play;

        public MovementEarth(Camera camera,
                             Transform[] earths)
        {
            _camera = camera;
            _earths = earths;
        }

        public void Initialize()
            => FillCancellationToken();

        public void StartMove()
        {
            _play = true;
            Move().Forget();
        }

        public void StopMove()
            => _play = false;

        private async UniTaskVoid Move()
        {
            var isPositionChanged = false;

            var screenCoordinateEarth = _camera.WorldToViewportPoint(_earths[_leftmostIndex].position +
                                                                            new Vector3(_earths[_leftmostIndex].lossyScale.x / 2, 0, 0));

            if (screenCoordinateEarth.x < 0)
            {
                _earths[_leftmostIndex].position = new Vector2(_earths[_leftmostIndex].position.x + _earths[_leftmostIndex].lossyScale.x * 2,
                                                                 _earths[_leftmostIndex].position.y);

                isPositionChanged = true;
            }

            await UniTask.NextFrame(_ct[_leftmostIndex]);

            if (!_ct[_leftmostIndex].IsCancellationRequested && _play)
            {
                if (isPositionChanged)
                    _leftmostIndex = (_leftmostIndex + 1) % _earths.Length;

                Move().Forget();
            }
        }

        private void FillCancellationToken()
        {
            _ct = new CancellationToken[_earths.Length];

            for (var i = 0; i < _earths.Length; i++)
            {
                _ct[i] = _earths[i].GetCancellationTokenOnDestroy();
            }
        }
    }
}
