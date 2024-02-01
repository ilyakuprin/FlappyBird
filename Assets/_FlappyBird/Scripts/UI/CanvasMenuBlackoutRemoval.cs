using DG.Tweening;
using System;
using UnityEngine.UI;
using Zenject;

namespace FlappyBird
{
    public class CanvasMenuBlackoutRemoval : IInitializable, IDisposable
    {
        public event Action Ended;

        private readonly CanvasDefeatBlackout _canvasDefeatBlackout;
        private readonly Image _panel;
        private readonly float _endValueFade = 0;
        private readonly float _endValueUnfade = 1;
        private readonly float _time;
        private readonly UiButton _uiButton;

        public CanvasMenuBlackoutRemoval(CanvasDefeatBlackout canvasDefeatBlackout,
                                         float time,
                                         Image panel,
                                         UiButton uiButton)
        {
            _canvasDefeatBlackout = canvasDefeatBlackout;
            _time = time;
            _panel = panel;
            _uiButton = uiButton;
        }

        public void Initialize()
        {
            _canvasDefeatBlackout.Ended += BlackoutRemoval;
            _uiButton.Pressed += Blackout;
        }

        private void BlackoutRemoval()
        {
            _panel.gameObject.SetActive(true);
            _panel.DOFade(_endValueFade, _time).OnKill(OffPanel);
        }

        private void Blackout()
        {
            _panel.gameObject.SetActive(true);
            _panel.DOFade(_endValueUnfade, _time).OnKill(EndUnfade);
        }

        private void OffPanel()
        {
            _panel.gameObject.SetActive(false);
        }

        private void EndUnfade()
        {
            OffPanel();
            Ended?.Invoke();
        }

        public void Dispose()
        {
            _canvasDefeatBlackout.Ended -= BlackoutRemoval;
            _uiButton.Pressed -= Blackout;
        }
    }
}
