using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class EnvironmentInstaller : MonoInstaller
    {
        [SerializeField] private SettingObstaclesConfig _config;

        [SerializeField] private Transform _prefab;
        [SerializeField] private Transform _parentColumn;
        [SerializeField] private Transform _parentEarth;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform[] _earth;

        private ObstacleFactory _obstacleFactory;
        private SettingPositionYObstacle _settingPositionYObstacle;

        public MovementObstacle Columns { get; private set; }
        public MovementObstacle Earth { get; private set; }

        public Transform ParentColumn { get => _parentColumn;}

        public override void InstallBindings()
        {
            BindSettingPositionYObstacle();
            BindObstacleFactory();
            BindSerializeField();
            BindMovementColumns();
            BindMovementEarth();
        }

        private void BindSettingPositionYObstacle()
        {
            _settingPositionYObstacle = new SettingPositionYObstacle(_config);
            Container.BindInterfacesAndSelfTo<SettingPositionYObstacle>().FromInstance(_settingPositionYObstacle).AsSingle();
        }

        private void BindObstacleFactory()
        {
            _obstacleFactory = new ObstacleFactory(_prefab, _parentColumn, Container, _settingPositionYObstacle);
            Container.BindInterfacesAndSelfTo<ObstacleFactory>().FromInstance(_obstacleFactory).AsSingle().NonLazy();
        }

        private void BindMovementColumns()
        {
            var movementColumns =
                new MovementColumns(_obstacleFactory, _config, _camera, _settingPositionYObstacle);
            Container.BindInterfacesAndSelfTo<MovementColumns>().FromInstance(movementColumns).AsSingle();

            Columns = new MovementObstacle(_config.Speed, _parentColumn, movementColumns);
            Container.BindInterfacesAndSelfTo<MovementObstacle>().FromInstance(Columns).AsTransient();
        }

        private void BindMovementEarth()
        {
            var earthCopy = new Transform[_earth.Length];
            _earth.CopyTo(earthCopy, 0);
            var movementEarth = new MovementEarth(_camera, earthCopy);
            Container.BindInterfacesAndSelfTo<MovementEarth>().FromInstance(movementEarth).AsSingle();

            Earth = new MovementObstacle(_config.Speed, _parentEarth, movementEarth);
            Container.BindInterfacesAndSelfTo<MovementObstacle>().FromInstance(Earth).AsTransient();
        }

        private void BindSerializeField()
        {
            Container.Bind<SettingObstaclesConfig>().FromInstance(_config).AsSingle();
        }
    }
}
