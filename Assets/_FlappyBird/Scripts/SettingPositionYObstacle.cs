using UnityEngine;

namespace Assets._FlappyBird
{
    public class SettingPositionYObstacle
    {
        private readonly float _upperPoint = 5;
        private readonly float _lowerPoint = -2.8f;

        public void Set(Transform transformObst)
        {
            float positionY = Random.Range(_lowerPoint, _upperPoint);
            transformObst.position = new Vector2(transformObst.position.x, positionY);
        }
    }
}
