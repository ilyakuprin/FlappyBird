using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class ObstacleFactory
    {
        private readonly Transform _prefab;
        private readonly Transform _parent;
        private readonly SettingPositionYObstacle _setting;
        private readonly DiContainer _diContainer;

        public ObstacleFactory(Transform prefab,
                               Transform parent,
                               DiContainer container,
                               SettingPositionYObstacle setting)
        {
            _prefab = prefab;
            _parent = parent;
            _diContainer = container;
            _setting = setting;
        }

        public Transform Get()
        {
            var obstacle = _diContainer.InstantiatePrefab(_prefab, 
                                                                  Vector3.zero,
                                                                  Quaternion.identity,
                                                                  _parent)
                                                .transform;

            _setting.Set(obstacle);

            return obstacle;
        }
    }
}
