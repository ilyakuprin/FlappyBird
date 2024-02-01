using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class SettingCameraWidth : IInitializable
    {
        public void Initialize()
        {
            var resolutions = Screen.resolutions;
            var height = resolutions[0].height;
            var width = height * 9 / 16;

            Screen.SetResolution(width, height, true);
        }
    }
}
