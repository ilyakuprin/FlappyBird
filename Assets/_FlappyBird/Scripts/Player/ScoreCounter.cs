using System;
using Zenject;

namespace FlappyBird
{
    public class ScoreCounter : IInitializable, IDisposable
    {
        public event Action<int> Added;

        private readonly PointCollector _collector;
        private readonly CanvasDefeatBlackout _defeat;

        public ScoreCounter(PointCollector collector,
                            CanvasDefeatBlackout defeat)
        {
            _collector = collector;
            _defeat = defeat;
        }

        public int Counter { get; private set; }

        public void Initialize()
        {
            _collector.Added += Add;
            _defeat.Ended += Reset;
        }

        private void Add()
        {
            Counter++;
            Added?.Invoke(Counter);
        }

        private void Reset()
        {
            Counter = 0;
            Added?.Invoke(Counter);
        }

        public void Dispose()
        {
            _collector.Added -= Add;
            _defeat.Ended -= Reset;
        }
    }
}
