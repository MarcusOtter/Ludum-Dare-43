using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    internal bool IsEnemyBullet { get; private set; }

    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _destroyDelay = 3f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, _destroyDelay);
    }

    internal void Shoot(bool isEnemyBullet)
    {
        IsEnemyBullet = isEnemyBullet;
        _rigidbody.AddForce(transform.up * _speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
