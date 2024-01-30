using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class MovementColumns : IInitializable, IMove
    {
        private readonly int _numberObstacles = 4;
        private readonly ObstacleFactory _factory;
        private readonly SettingObstaclesConfig _config;
        private readonly Camera _camera;
        private readonly SettingPositionYObstacle _settingPosition;

        private Transform[] _obstacles;
        private CancellationToken[] _ct;
        private int _leftmostObstacle;
        private int _rightmostObstacle;
        private bool _play;

        public MovementColumns(ObstacleFactory factory,
                               SettingObstaclesConfig config,
                               Camera camera,
                               SettingPositionYObstacle settingPosition)
        {
            _factory = factory;
            _config = config;
            _camera = camera;
            _settingPosition = settingPosition;
        }

        public void Initialize()
        {
            FillArray();
            FillCancellationToken();

            _rightmostObstacle = _numberObstacles - 1;
        }

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

            var obstacle = _obstacles[_leftmostObstacle];
            var screenCoordinate = _camera.WorldToViewportPoint(obstacle.position +
                                                                new Vector3(obstacle.lossyScale.x / 2, 0, 0));

            if (screenCoordinate.x < 0)
            {
                obstacle.position = _obstacles[_rightmostObstacle].position +
                                    new Vector3(_config.HorizontalDistance, 0, 0);

                _settingPosition.Set(obstacle);

                isPositionChanged = true;
            }

            await UniTask.NextFrame(_ct[_leftmostObstacle]);

            if (!_ct[_leftmostObstacle].IsCancellationRequested && _play)
            {
                if (isPositionChanged)
                {
                    _leftmostObstacle = (_leftmostObstacle + 1) % _numberObstacles;
                    _rightmostObstacle = (_rightmostObstacle + 1) % _numberObstacles;
                }

                Move().Forget();
            }
        }

        private void FillArray()
        {
            _obstacles = new Transform[_numberObstacles];

            for (var i = 0; i < _numberObstacles; i++)
            {
                var obstacle = _factory.Get();
                obstacle.position += obstacle.parent.position + i * _config.HorizontalDistance * Vector3.right;
                _obstacles[i] = obstacle;
            }
        }

        private void FillCancellationToken()
        {
            _ct = new CancellationToken[_numberObstacles];

            for (var i = 0; i < _numberObstacles; i++)
            {
                _ct[i] = _obstacles[i].GetCancellationTokenOnDestroy();
            }
        }
    }
}
