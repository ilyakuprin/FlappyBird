using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class SimulatedRigidbody : IInitializable
    {
        private readonly Rigidbody2D _player;
        private readonly CanvasMenuBlackoutRemoval _menu;

        public SimulatedRigidbody(Rigidbody2D player,
                                  CanvasMenuBlackoutRemoval menu)
        {
            _player = player;
            _menu = menu;
        }

        public void Initialize()
            => _menu.Ended += OffSimulated;

        public void OnSimulated()
            => SetSimulated(true);

        private void OffSimulated()
            => SetSimulated(false);

        private void SetSimulated(bool value)
            => _player.simulated = value;
    }
}
