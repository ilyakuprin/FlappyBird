using System;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class ActivationMenuCanvas : IInitializable, IDisposable
    {
        private readonly CanvasDefeatBlackout _defeat;
        private readonly Canvas _canvasMenu;
        private readonly CanvasMenuBlackoutRemoval _menu;

        public ActivationMenuCanvas(CanvasDefeatBlackout defeat,
                                    Canvas canvasMenu,
                                    CanvasMenuBlackoutRemoval menu)
        {
            _defeat = defeat;
            _canvasMenu = canvasMenu;
            _menu = menu;
        }

        public void Initialize()
        {
            _defeat.Ended += On;
            _menu.Ended += Off;
        }

        private void On()
            => SetActive(true);

        private void Off()
            => SetActive(false);

        private void SetActive(bool value)
            => _canvasMenu.gameObject.SetActive(value);

        public void Dispose()
        {
            _defeat.Ended -= On;
            _menu.Ended -= Off;
        }
    }
}
