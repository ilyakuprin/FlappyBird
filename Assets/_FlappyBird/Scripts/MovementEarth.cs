using System.Collections;
using UnityEngine;

namespace Assets._FlappyBird
{
    public class MovementEarth : MonoBehaviour
    {
        [SerializeField] private Transform _earthParent;
        [SerializeField] private Transform _earth1;
        [SerializeField] private Transform _earth2;
        [SerializeField] private float _speed;
        [SerializeField] private Camera _camera;

        private Coroutine _move;

        private void Start()
        {
            _move = StartCoroutine(Move());
        }

        public void StopMove()
        {
            if (_move != null)
                StopCoroutine(_move);
        }

        private IEnumerator Move()
        {
            while (true)
            {
                _earthParent.position += _speed * Time.deltaTime * Vector3.left;

                Vector3 screenCoordinateEarth1 = _camera.WorldToViewportPoint(_earth1.position +
                                                                              new Vector3(_earth1.lossyScale.x / 2, 0, 0));
                Vector3 screenCoordinateEarth2 = _camera.WorldToViewportPoint(_earth2.position +
                                                                              new Vector3(_earth2.lossyScale.x / 2, 0, 0));

                if (screenCoordinateEarth1.x < 0)
                {
                    _earth1.position = new Vector2(_earth1.position.x + _earth1.lossyScale.x * 2, _earth1.position.y);
                }
                else if (screenCoordinateEarth2.x < 0)
                {
                    _earth2.position = new Vector2(_earth2.position.x + _earth2.lossyScale.x * 2, _earth2.position.y);
                }

                yield return null;
            }
        }
    }
}
