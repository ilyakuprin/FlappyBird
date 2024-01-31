using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class GameStateInstaller : MonoInstaller
    {
        [SerializeField] private EnvironmentInstaller _environment;
        [SerializeField] private PlayerInstaller _playerInstaller;

        public override void InstallBindings()
        {
            BindDefeat();
        }

        private void BindDefeat()
        {
            var moves = new IMove[] { _environment.Columns, _environment.Earth };
            var defeat = new Defeat(_playerInstaller.Death, moves, _playerInstaller.PlayerBouncing);
            Container.BindInterfacesAndSelfTo<Defeat>().FromInstance(defeat).AsSingle();
        }
    }
}
