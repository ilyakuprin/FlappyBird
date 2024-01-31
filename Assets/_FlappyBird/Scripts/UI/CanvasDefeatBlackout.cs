using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FlappyBird
{
    public class CanvasDefeatBlackout : IInitializable, IDisposable
    {
        public event Action Ended;

        private readonly Image _panel;
        private readonly float _endValueUnfade = 1;
        private readonly float _time;
        private readonly GameOverOkButton _okButton;

        public CanvasDefeatBlackout(Image panel,
                                    GameOverOkButton okButton,
                                    float time)
        {
            _panel = panel;
            _okButton = okButton;
            _time = time;
        }

        public void Initialize()
            => _okButton.Pressed += Blackout;

        private void Blackout()
        {
            _panel.enabled = true;
            _panel.DOFade(_endValueUnfade, _time).OnKill(EndFade);
        }

        private void EndFade()
        {
            Ended?.Invoke();
            _panel.color = new Color(0, 0, 0, 0);
            _panel.enabled = false;
        }

        public void Dispose()
            => _okButton.Pressed -= Blackout;
    }
}
