using UnityEngine;

namespace FlappyBird
{
    public class SettingPositionYObstacle
    {
        private readonly float _upperPoint;
        private readonly float _lowerPoint;

        public SettingPositionYObstacle(SettingObstaclesConfig config)
        {
            _upperPoint = config.UpperPointY;
            _lowerPoint = config.LowerPointY;
        }

        public void Set(Transform transformObst)
        {
            var positionY = Random.Range(_lowerPoint, _upperPoint);
            transformObst.position = new Vector2(transformObst.position.x, positionY);
        }
    }
}
