using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird
{
    public class FadingImage
    {
        private readonly Image _image;
        private readonly float _endValue = 0;
        private readonly float _timeFade;

        public FadingImage(Image image,
                           float time)
        {
            _image = image;
            _timeFade = time;
        }

        public void ResetColor()
        {
            _image.color = new Color(1, 1, 1, 1);
        }

        public void Fade()
        {
            _image.DOFade(_endValue, _timeFade);
        }
    }
}
