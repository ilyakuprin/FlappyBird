using System;
using TMPro;
using Zenject;

namespace FlappyBird
{
    public class ViewScoreCounter : IInitializable, IDisposable
    {
        private readonly ScoreCounter _scoreCounter;
        private readonly TextMeshProUGUI _text;

        public ViewScoreCounter(ScoreCounter scoreCounter,
                                TextMeshProUGUI text)
        {
            _scoreCounter = scoreCounter;
            _text = text;
        }

        public void Initialize()
        {
            _scoreCounter.Added += SetText;
        }

        private void SetText(int value)
            => _text.text = value.ToString();

        public void Dispose()
        {
            _scoreCounter.Added -= SetText;
        }
    }
}
