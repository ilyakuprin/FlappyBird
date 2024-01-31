using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _config;
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

        public PlayerInput PlayerInput { get; private set; }
        public PlayerDeath Death { get; private set; }
        public PlayerBouncing PlayerBouncing { get; private set; }

        public override void InstallBindings()
        {
            BindSerializeField();

            BindPlayerInput();
            BindPlayerBouncing();
            BindPlayerDeath();
        }

        private void BindSerializeField()
        {
            Container.Bind<Rigidbody2D>().FromInstance(Rigidbody).AsSingle();
            Container.Bind<PlayerConfig>().FromInstance(_config).AsSingle();
        }

        private void BindPlayerBouncing()
        {
            PlayerBouncing = new PlayerBouncing(PlayerInput, Rigidbody, _config);
            Container.BindInterfacesAndSelfTo<PlayerBouncing>().FromInstance(PlayerBouncing).AsSingle().NonLazy();
        }

        private void BindPlayerInput()
        {
            PlayerInput = new PlayerInput();
            Container.BindInterfacesAndSelfTo<PlayerInput>().FromInstance(PlayerInput).AsSingle();
        }

        private void BindPlayerDeath()
        {
            Death = new PlayerDeath(Rigidbody);
            Container.BindInterfacesAndSelfTo<PlayerDeath>().FromInstance(Death).AsSingle();
        }
    }
}
