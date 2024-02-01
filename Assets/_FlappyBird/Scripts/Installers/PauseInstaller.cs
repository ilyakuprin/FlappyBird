using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

namespace FlappyBird
{
    public class PauseInstaller : MonoInstaller
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private GameObject _pause;
        [SerializeField] private Button _stop;
        [SerializeField] private Button _back;
        [SerializeField] private Button _music;

        private UiButton _stopUi;
        private UiButton _pauseUi;

        public override void InstallBindings()
        {
            BindPause();
            BindBack();
            BindPauseWindowShowing();
            BindMusic();
        }

        private void BindPause()
        {
            _stopUi = new UiButton(_stop);
            Container.BindInterfacesAndSelfTo<UiButton>().FromInstance(_stopUi).AsTransient();

            var pause = new Pause(_stopUi);
            Container.BindInterfacesAndSelfTo<Pause>().FromInstance(pause).AsSingle();
        }

        private void BindBack()
        {
            _pauseUi = new UiButton(_back);
            Container.BindInterfacesAndSelfTo<UiButton>().FromInstance(_pauseUi).AsTransient();

            var back = new Back(_pauseUi);
            Container.BindInterfacesAndSelfTo<Back>().FromInstance(back).AsSingle();
        }

        private void BindPauseWindowShowing()
        {
            var pauseWindowShowing = new PauseWindowShowing(_pause, _stopUi, _pauseUi);
            Container.BindInterfacesAndSelfTo<PauseWindowShowing>().FromInstance(pauseWindowShowing).AsSingle();
        }

        private void BindMusic()
        {
            var musicUi = new UiButton(_music);
            Container.BindInterfacesAndSelfTo<UiButton>().FromInstance(musicUi).AsTransient();

            var music = new Music(musicUi, _mixer);
            Container.BindInterfacesAndSelfTo<Music>().FromInstance(music).AsSingle();
        }
    }
}
