using System;
using Zenject;

namespace FlappyBird
{
    public class ShowingResults : IInitializable, IDisposable
    {
        private readonly ViewResult _viewResult;
        private readonly ScoreCounter _scoreCounter;
        private int _bestResult;

        public ShowingResults(ViewResult viewResult,
                              ScoreCounter scoreCounter)
        {
            _viewResult = viewResult;
            _scoreCounter = scoreCounter;
        }

        public void Initialize()
            => _scoreCounter.Added += Show;

        private void Show(int value)
        {
            _viewResult.Current.text = value.ToString();

            if (value >= _bestResult)
            {
                _bestResult = value;
                _viewResult.Best.text = value.ToString();
            }
        }

        public void Dispose()
            => _scoreCounter.Added -= Show;
    }
}
