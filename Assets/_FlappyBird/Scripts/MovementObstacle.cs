using System.Collections;
using UnityEngine;

namespace Assets._FlappyBird
{
    public class MovementObstacle : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _obstacles;

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
                _obstacles.position += _speed * Time.deltaTime * Vector3.left;

                yield return null;
            }
        }
    }
}
