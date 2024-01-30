using System;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class PlayerInput : ITickable
    {
        public event Action<InputData> Inputted;
        private InputData _inputData;

        public void Tick()
        {
            _inputData = new InputData()
            {
                Jump = Input.GetMouseButtonDown(0)
            };

            Inputted?.Invoke(_inputData);
        }
    }
}
