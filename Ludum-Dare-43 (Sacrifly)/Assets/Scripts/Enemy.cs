using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _minRotationOffset;
    [SerializeField] private float _maxRotationOffset;
    [SerializeField] protected float _movementSpeed;

    private float GetRandomRotationOffset => Random.Range(_minRotationOffset, _maxRotationOffset);

    protected Rigidbody2D Rigidbody;

    private void Awake()
    {
        transform.Rotate(0, 0, GetRandomRotationOffset);
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        Rigidbody.velocity = transform.up * _movementSpeed;
    }

    protected virtual void Die(bool killedByPlayer)
    {
        Destroy(gameObject);

        if (killedByPlayer)
        {
            FindObjectOfType<Stats>().IncrementKillCount();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        var collider = col.collider;

        if (collider.CompareTag(EnvironmentVariables.BulletTag))
        {
            if (!collider.GetComponent<Bullet>().IsEnemyBullet)
            {
                Die(killedByPlayer: true);
            }
        }
        else if (collider.CompareTag(EnvironmentVariables.EnemyBoundTag))
        {
            Die(killedByPlayer: false);
        }
        else if (collider.CompareTag(EnvironmentVariables.EnemyTag))
        {
            // Explode
        }
    }
}
