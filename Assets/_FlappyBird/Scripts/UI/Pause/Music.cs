using System;
using UnityEngine.Audio;
using Zenject;

namespace FlappyBird
{
    public class Music : IInitializable, IDisposable
    {
        private readonly UiButton _button;
        private readonly AudioMixer _mixer;
        private readonly string _parameter = "Master";
        private readonly float _lowerVolumeBound = -80f;
        private readonly float _upperVolumeBound = 0f;
        private bool _isMute;

        public Music(UiButton button,
                     AudioMixer mixer)
        {
            _button = button;
            _mixer = mixer;
        }

        public void Initialize()
        {
            _button.Pressed += ChangeValue;
        }

        private void ChangeValue()
        {
            if (_isMute)
                UnMute();
            else
                Mute();

            _isMute = !_isMute;
        }

        private void Mute()
            => SetVolume(_lowerVolumeBound);

        private void UnMute()
            => SetVolume(_upperVolumeBound);

        private void SetVolume(float value)
            => _mixer.SetFloat(_parameter, value);

        public void Dispose()
        {
            _button.Pressed -= ChangeValue;
        }
    }
}
