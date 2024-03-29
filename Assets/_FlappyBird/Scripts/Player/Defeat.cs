using System;
using Zenject;

namespace FlappyBird
{
    public class Defeat : IInitializable, IDisposable
    {
        public event Action Lost;

        private readonly PlayerDeath _playerDeath;
        private readonly MovementObstacle[] _moves;
        private readonly PlayerBouncing _bouncing;

        public Defeat(PlayerDeath playerDeath,
                      MovementObstacle[] moves,
                      PlayerBouncing bouncing)
        {
            _playerDeath = playerDeath;
            _moves = moves;
            _bouncing = bouncing;
        }

        public void Initialize()
            => _playerDeath.Died += Lose;

        private void Lose()
        {
            foreach (var move in _moves)
                move.StopMove();

            _bouncing.Dispose();

            Lost?.Invoke();
        }

        public void Dispose()
            => _playerDeath.Died -= Lose;
    }
}
