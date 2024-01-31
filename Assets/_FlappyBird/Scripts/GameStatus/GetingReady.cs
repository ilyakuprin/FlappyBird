using System;
using Zenject;

namespace FlappyBird
{
    public class ActivationMovement : IInitializable, IDisposable
    {
        private readonly MovementObstacle _earth;
        private readonly PlayerInput _playerInput;
        private readonly FadingImage[] _fadingImages;
        private readonly SimulatedRigidbody _simulated;
        private readonly MovementObstacle _column;
        private readonly CanvasMenuBlackoutRemoval _menu;
        private readonly PlayerBouncing _bouncing;
        private readonly PlayerDeath _playerDeath;

        public ActivationMovement(MovementObstacle earth,
                                  PlayerInput playerInput,
                                  FadingImage[] fadingImages,
                                  SimulatedRigidbody simulated,
                                  MovementObstacle column,
                                  CanvasMenuBlackoutRemoval menu,
                                  PlayerBouncing bouncing,
                                  PlayerDeath playerDeath)
        {
            _earth = earth;
            _playerInput = playerInput;
            _fadingImages = fadingImages;
            _simulated = simulated;
            _column = column;
            _menu = menu;
            _bouncing = bouncing;
            _playerDeath = playerDeath;
        }

        public void Initialize()
        {
            MoveEarth();
            _menu.Ended += CanClick;
            _menu.Ended += ResetImageColor;
        }

        private void MoveEarth()
        {
            _earth.StartMove();
        }

        private void ResetImageColor()
        {
            foreach (var fadingImage in _fadingImages)
                fadingImage.ResetColor();
        }

        private void CanClick()
        {
            _playerInput.Inputted += WaitPress;
        }

        private void WaitPress(InputData inputData)
        {
            if (inputData.Jump)
            {
                foreach (var fadingImage in _fadingImages)
                    fadingImage.Fade();

                _simulated.OnSimulated();
                _column.StartMove();
                _bouncing.Subscribe();
                _playerDeath.Start();
                UnsubscribeInput();
            }
        }

        private void UnsubscribeInput()
            => _playerInput.Inputted -= WaitPress;

        public void Dispose()
        {
            UnsubscribeInput();
            _menu.Ended -= CanClick;
            _menu.Ended -= ResetImageColor;
        }
    }
}
