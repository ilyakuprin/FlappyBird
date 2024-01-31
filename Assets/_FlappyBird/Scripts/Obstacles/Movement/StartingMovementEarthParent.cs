using System;
using Zenject;

namespace FlappyBird
{
    public class StartingMovementEarthParent : IInitializable, IDisposable
    {
        private readonly CanvasDefeatBlackout _defeat;
        private readonly MovementObstacle _earth;

        public StartingMovementEarthParent(CanvasDefeatBlackout defeat,
                                           MovementObstacle earth)
        {
            _defeat = defeat;
            _earth = earth;
        }

        public void Initialize()
        {
            _defeat.Ended += Move;
        }

        private void Move()
        {
            _earth.StartMove();
        }

        public void Dispose()
        {
            _defeat.Ended -= Move;
        }
    }
}
