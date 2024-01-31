using System;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class ActivationDefeatCanvas : IInitializable, IDisposable
    {
        private readonly Defeat _defeat;
        private readonly Canvas _defeatCanvas;
        private readonly CanvasDefeatBlackout _canvasDefeatBlackout;

        public ActivationDefeatCanvas(Defeat defeat,
                                      Canvas defeatCanvas,
                                      CanvasDefeatBlackout canvasDefeatBlackout)
        {
            _defeat = defeat;
            _defeatCanvas = defeatCanvas;
            _canvasDefeatBlackout = canvasDefeatBlackout;
        }

        public void Initialize()
        {
            _defeat.Lost += On;
            _canvasDefeatBlackout.Ended += Off;
        } 

        private void On()
            => SetActive(true);

        private void Off()
            => SetActive(false);

        private void SetActive(bool value)
            => _defeatCanvas.gameObject.SetActive(value);

        public void Dispose()
        {
            _defeat.Lost -= On;
            _canvasDefeatBlackout.Ended -= Off;
        }
    }
}
