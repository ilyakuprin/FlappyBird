using System;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class ActivationGetReadyCanvas : IInitializable, IDisposable
    {
        private readonly Canvas _canvasGetReady;
        private readonly CanvasMenuBlackoutRemoval _menu;
        private readonly CanvasDefeatBlackout _defeat;

        public ActivationGetReadyCanvas(Canvas canvasGetReady,
                                        CanvasMenuBlackoutRemoval menu,
                                        CanvasDefeatBlackout defeat)
        {
            _canvasGetReady = canvasGetReady;
            _menu = menu;
            _defeat = defeat;
        }

        public void Initialize()
        {
            _menu.Ended += On;
            _defeat.Ended += Off;
        }

        private void On()
            => SetActive(true);

        private void Off()
            => SetActive(false);

        private void SetActive(bool value)
            => _canvasGetReady.gameObject.SetActive(value);

        public void Dispose()
        {
            _menu.Ended -= On;
            _defeat.Ended -= Off;
        }
    }
}
