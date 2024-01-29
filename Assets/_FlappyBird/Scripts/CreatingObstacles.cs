using UnityEngine;

namespace Assets._FlappyBird
{
    public class CreatingObstacles : MonoBehaviour
    {
        [SerializeField] private Transform _prefab;
        [SerializeField] private SettingObstaclesConfig _config;
        [SerializeField] private Transform _parent;
        private readonly int _numberObstacles = 4;
        private readonly SettingPositionYObstacle _setting = new SettingPositionYObstacle();
        [SerializeField] private Camera _camera;

        private Transform[] _obstacles;
        private int _leftmostObstacle = 0;
        private int _rightmostObstacle;

        private void Awake()
        {
            _rightmostObstacle = _numberObstacles - 1;

            _obstacles = new Transform[_numberObstacles];

            for (int i = 0; i < _numberObstacles; i++)
            {
                Transform obstacle = Instantiate(_prefab,
                                                 _parent.position + i * _config.HorizontalDistance * Vector3.right,
                                                 Quaternion.identity,
                                                 _parent);

                _setting.Set(obstacle);

                _obstacles[i] = obstacle;
            }
        }

        private void Update()
        {
            Transform obstacle = _obstacles[_leftmostObstacle];
            Vector3 screenCoordinate = _camera.WorldToViewportPoint(obstacle.position +
                                                                    new Vector3(obstacle.lossyScale.x / 2, 0, 0));

            if (screenCoordinate.x < 0)
            {
                obstacle.position = _obstacles[_rightmostObstacle].position +
                                    new Vector3(_config.HorizontalDistance, 0, 0);

                _setting.Set(obstacle);

                _leftmostObstacle = (_leftmostObstacle + 1) % _numberObstacles;
                _rightmostObstacle = (_rightmostObstacle + 1) % _numberObstacles;
            }
        }
    }
}
