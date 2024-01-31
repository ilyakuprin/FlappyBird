using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FlappyBird
{
    public class PauseInstaller : MonoInstaller
    {
        [SerializeField] private Button _stop;
        [SerializeField] private Button _play;
        [SerializeField] private Button _music;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Pause>().AsSingle().NonLazy();
        }
    }
}
