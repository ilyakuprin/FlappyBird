using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FlappyBird
{
    public class CanvasGetReadyUnFade : IInitializable, IDisposable
    {
        public event Action Ended;

        private readonly CanvasMenuBlackoutRemoval _menu;
        private readonly float _endValueFade = 0;
        private readonly Image _panel;
        private readonly float _time;

        public CanvasGetReadyUnFade(CanvasMenuBlackoutRemoval menu,
                                    Image panel,
                                    float time)
        {
            _menu = menu;
            _panel = panel;
            _time = time;
        }

        public void Initialize()
        {
            _menu.Ended += Fade;
        }

        private void Fade()
        {
            _panel.gameObject.SetActive(true);
            _panel.color = new Color(0, 0, 0, 1);
            _panel.DOFade(_endValueFade, _time).OnKill(EndFade);
        }

        private void EndFade()
        {
            _panel.gameObject.SetActive(false);
            Ended?.Invoke();
        }

        public void Dispose()
        {
            _menu.Ended -= Fade;
        }
    }
}
