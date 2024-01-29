using UnityEngine;

public class PlayerBouncing : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _force;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _rigidbody.velocity = new Vector2(0, _force);
    }
}
