using System;
using UnityEngine;
using Zenject;

namespace FlappyBird
{
    public class ResettingColumns : IInitializable, IDisposable
    {
        private readonly Transform _columns;
        private readonly CanvasDefeatBlackout _defeat;
        private Vector3 _startPosition;

        public ResettingColumns(Transform columns,
                                CanvasDefeatBlackout defeat)
        {
            _columns = columns;
            _defeat = defeat;
        }

        public void Initialize()
        {
            _startPosition = _columns.position;
            _defeat.Ended += Reset;
        }

        private void Reset()
            => _columns.position = _startPosition;

        public void Dispose()
            => _defeat.Ended -= Reset;
    }
}
