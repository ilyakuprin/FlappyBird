using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class GameStateInstaller : MonoInstaller
    {
        [SerializeField] private EnvironmentInstaller _environment;
    }
}
