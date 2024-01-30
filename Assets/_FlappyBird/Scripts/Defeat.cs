using System;
using Zenject;

namespace FlappyBird
{
    public class Defeat : IInitializable, IDisposable
    {
        private readonly PlayerDeath _playerDeath;

        public Defeat(PlayerDeath playerDeath)
        {
            _playerDeath = playerDeath;
        }

        public void Initialize()
        {
            _playerDeath.Died += Lose;
        }

        private void Lose()
        {

        }

        public void Dispose()
        {
            _playerDeath.Died -= Lose;
        }
    }
}
