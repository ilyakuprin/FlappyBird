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

            var screenCoordinateEarth = _camera.WorldToViewportPoint(_earths[_leftmostIndex].position);
            
            if (screenCoordinateEarth.x < 0)
            {
                var distanceBetweenEarth = _earths[GetNextIndex(_leftmostIndex)].localPosition.x -
                                           _earths[_leftmostIndex].localPosition.x;

                _earths[_leftmostIndex].localPosition = new Vector2(_earths[_leftmostIndex].localPosition.x + distanceBetweenEarth * 2,
                                                                  _earths[_leftmostIndex].localPosition.y);

                isPositionChanged = true;
            }

            await UniTask.NextFrame(_ct[_leftmostIndex]);

            if (!_ct[_leftmostIndex].IsCancellationRequested && _play)
            {
                if (isPositionChanged)
                    _leftmostIndex = GetNextIndex(_leftmostIndex);
                
                Move().Forget();
            }
        }

        private int GetNextIndex(int current)
            => (current + 1) % _earths.Length;

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
