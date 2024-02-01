using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _config;
        [field: SerializeField] public Rigidbody2D Player { get; private set; }

        public PlayerInput PlayerInput { get; private set; }
        public PlayerDeath Death { get; private set; }
        public PlayerBouncing PlayerBouncing { get; private set; }

        public override void InstallBindings()
        {
            BindSerializeField();

            BindPlayerInput();
            BindPlayerBouncing();
            BindPlayerDeath();
            BindPointCollector();
            BindScoreCounter();
            BindSettingCameraWidth();
        }

        private void BindSerializeField()
        {
            Container.Bind<Rigidbody2D>().FromInstance(Player).AsSingle();
            Container.Bind<PlayerConfig>().FromInstance(_config).AsSingle();
        }

        private void BindPlayerBouncing()
        {
            PlayerBouncing = new PlayerBouncing(PlayerInput, Player, _config);
            Container.BindInterfacesAndSelfTo<PlayerBouncing>().FromInstance(PlayerBouncing).AsSingle().NonLazy();
        }

        private void BindPlayerInput()
        {
            PlayerInput = new PlayerInput();
            Container.BindInterfacesAndSelfTo<PlayerInput>().FromInstance(PlayerInput).AsSingle();
        }

        private void BindPlayerDeath()
        {
            Death = new PlayerDeath(Player);
            Container.BindInterfacesAndSelfTo<PlayerDeath>().FromInstance(Death).AsSingle();
        }

        private void BindPointCollector()
        {
            Container.BindInterfacesAndSelfTo<PointCollector>().AsSingle();
        }

        private void BindScoreCounter()
        {
            Container.BindInterfacesAndSelfTo<ScoreCounter>().AsSingle();
        }

        private void BindSettingCameraWidth()
        {
            Container.BindInterfacesAndSelfTo<SettingCameraWidth>().AsSingle();
        }
    }
}
