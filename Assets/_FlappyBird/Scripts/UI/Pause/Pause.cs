using UnityEngine;

namespace FlappyBird
{
    public class Pause
    {
        public void Stop()
        {
            Time.timeScale = 0;
        }

        public void Play()
        {
            Time.timeScale = 1;
        }
    }
}
