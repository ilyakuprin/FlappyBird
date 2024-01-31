using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private PlayerConfig _config;

        private PlayerInput _playerInput;

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
            Container.Bind<Rigidbody2D>().FromInstance(_rigidbody).AsSingle();
            Container.Bind<PlayerConfig>().FromInstance(_config).AsSingle();
        }

        private void BindPlayerBouncing()
        {
            PlayerBouncing = new PlayerBouncing(_playerInput, _rigidbody, _config);
            Container.BindInterfacesAndSelfTo<PlayerBouncing>().FromInstance(PlayerBouncing).AsSingle();
        }

        private void BindPlayerInput()
        {
            _playerInput = new PlayerInput();
            Container.BindInterfacesAndSelfTo<PlayerInput>().FromInstance(_playerInput).AsSingle();
        }

        private void BindPlayerDeath()
        {
            Death = new PlayerDeath(_rigidbody);
            Container.BindInterfacesAndSelfTo<PlayerDeath>().FromInstance(Death).AsSingle();
        }
    }
}
