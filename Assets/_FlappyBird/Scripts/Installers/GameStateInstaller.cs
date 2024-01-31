using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FlappyBird
{
    public class GameStateInstaller : MonoInstaller
    {
        [SerializeField] private EnvironmentInstaller _environment;
        [SerializeField] private PlayerInstaller _playerInstaller;
        [SerializeField] private Image[] _gettingReadyImages;
        [SerializeField] private UiConfig _uiConfig;
        [SerializeField] private Animator _animator;
        [SerializeField] private Canvas _defeatCanvas;
        [SerializeField] private Image _panelDefeatCanvas;
        [SerializeField] private Button _okButtonDefeat;
        [SerializeField] private Canvas _menuCanvas;
        [SerializeField] private Image _panelMenuCanvas;
        [SerializeField] private Button _playButton;
        [SerializeField] private Image _panelGetReadyCanvas;
        [SerializeField] private Canvas _getReadyCanvas;

        private Defeat _defeat;
        private GameOverOkButton _gameOverOkButton;
        private CanvasDefeatBlackout _canvasDefeatBlackout;
        private PlayButton _play;
        private CanvasMenuBlackoutRemoval _menu;
        private CanvasGetReadyUnFade _canvasGetReadyUnFade;
        private SimulatedRigidbody _simulatedRigidbody;

        public override void InstallBindings()
        {
            BindAnimator();
            BindDefeat();
            BindStoppingAnimation();
            BindGameOverOkButton();
            BindCanvasDefeatBlackout();
            BindActivationDefeatCanvas();
            BindPlayButton();
            BindCanvasMenuBlackoutRemoval();
            BindResettingColumns();
            BindResettingPlayer();
            BindActivationPlayer();
            BindCanvasGetReadyUnFade();
            BindActivationMenuCanvas();
            BindActivationGetReadyCanvas();
            BindSimulatedRigidbody();
            BindGettingReady();
            BindStartingMovementEarthParent();
        }

        private void BindAnimator()
        {
            Container.Bind<Animator>().FromInstance(_animator).AsSingle();
        }

        private void BindDefeat()
        {
            var moves = new [] { _environment.Columns, _environment.Earth };
            _defeat = new Defeat(_playerInstaller.Death, moves, _playerInstaller.PlayerBouncing);
            Container.BindInterfacesAndSelfTo<Defeat>().FromInstance(_defeat).AsSingle();
        }

        private void BindStoppingAnimation()
        {
            Container.BindInterfacesAndSelfTo<PlayerAnimation>().AsSingle();
        }

        private void BindGameOverOkButton()
        {
            _gameOverOkButton = new GameOverOkButton(_okButtonDefeat);
            Container.BindInterfacesAndSelfTo<GameOverOkButton>().FromInstance(_gameOverOkButton).AsSingle();
        }

        private void BindCanvasDefeatBlackout()
        {
            _canvasDefeatBlackout = new CanvasDefeatBlackout(_panelDefeatCanvas, _gameOverOkButton, _uiConfig.TimeBlackout);
            Container.BindInterfacesAndSelfTo<CanvasDefeatBlackout>().FromInstance(_canvasDefeatBlackout).AsSingle();
        }

        private void BindActivationDefeatCanvas()
        {
            var activationDefeatCanvas = new ActivationDefeatCanvas(_defeat, _defeatCanvas, _canvasDefeatBlackout);
            Container.BindInterfacesAndSelfTo<ActivationDefeatCanvas>().FromInstance(activationDefeatCanvas).AsSingle();
        }

        private void BindPlayButton()
        {
            _play = new PlayButton(_playButton);
            Container.BindInterfacesAndSelfTo<PlayButton>().FromInstance(_play).AsSingle();
        }

        private void BindCanvasMenuBlackoutRemoval()
        {
            _menu = new CanvasMenuBlackoutRemoval(_canvasDefeatBlackout, _uiConfig.TimeBlackout, _panelMenuCanvas, _play);
            Container.BindInterfacesAndSelfTo<CanvasMenuBlackoutRemoval>().FromInstance(_menu).AsSingle();
        }

        private void BindResettingColumns()
        {
            var resettingColumns = new ResettingColumns(_environment.ParentColumn, _canvasDefeatBlackout);
            Container.BindInterfacesAndSelfTo<ResettingColumns>().FromInstance(resettingColumns).AsSingle();
        }

        private void BindResettingPlayer()
        {
            Container.BindInterfacesAndSelfTo<ResettingPlayer>().AsSingle();
        }

        private void BindActivationPlayer()
        {
            Container.BindInterfacesAndSelfTo<ActivationPlayer>().AsSingle();
        }

        private void BindCanvasGetReadyUnFade()
        {
            _canvasGetReadyUnFade = new CanvasGetReadyUnFade(_menu, _panelGetReadyCanvas, _uiConfig.TimeBlackout);
            Container.BindInterfacesAndSelfTo<CanvasGetReadyUnFade>().FromInstance(_canvasGetReadyUnFade).AsSingle();
        }

        private void BindActivationMenuCanvas()
        {
            var activationMenuCanvas = new ActivationMenuCanvas(_canvasDefeatBlackout, _menuCanvas, _menu);
            Container.BindInterfacesAndSelfTo<ActivationMenuCanvas>().FromInstance(activationMenuCanvas).AsSingle();
        }

        private void BindActivationGetReadyCanvas()
        {
            var activationGetReadyCanvas = new ActivationGetReadyCanvas(_getReadyCanvas, _menu, _canvasDefeatBlackout);
            Container.BindInterfacesAndSelfTo<ActivationGetReadyCanvas>().FromInstance(activationGetReadyCanvas).AsSingle();
        }

        private void BindSimulatedRigidbody()
        {
            _simulatedRigidbody = new SimulatedRigidbody(_playerInstaller.Rigidbody, _menu);
            Container.BindInterfacesAndSelfTo<SimulatedRigidbody>().FromInstance(_simulatedRigidbody).AsSingle();
        }

        private void BindGettingReady()
        {
            var fadeImgs = new FadingImage[_gettingReadyImages.Length];

            for (var i = 0; i < _gettingReadyImages.Length; i++)
            {
                fadeImgs[i] = new FadingImage(_gettingReadyImages[i], _uiConfig.TimeFadingGetReady);
            }

            var gettingReady = new ActivationMovement(_environment.Earth,
                _playerInstaller.PlayerInput,
                fadeImgs,
                _simulatedRigidbody,
                _environment.Columns,
                _menu,
                _playerInstaller.PlayerBouncing,
                _playerInstaller.Death);
            Container.BindInterfacesAndSelfTo<ActivationMovement>().FromInstance(gettingReady).AsSingle();
        }

        private void BindStartingMovementEarthParent()
        {
            var startingMovementEarthParent =
                new StartingMovementEarthParent(_canvasDefeatBlackout, _environment.Earth);
            Container.BindInterfacesAndSelfTo<StartingMovementEarthParent>().FromInstance(startingMovementEarthParent).AsSingle();
        }
    }
}
