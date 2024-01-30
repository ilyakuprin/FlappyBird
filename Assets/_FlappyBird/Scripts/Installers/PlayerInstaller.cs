using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private PlayerConfig _config;

        public override void InstallBindings()
        {
            BindSerializeField();
            BindInterfaces();
        }

        private void BindSerializeField()
        {
            Container.Bind<Rigidbody2D>().FromInstance(_rigidbody).AsSingle();
            Container.Bind<PlayerConfig>().FromInstance(_config).AsSingle();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerBouncing>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerDeath>().AsSingle();
        }
    }
}
