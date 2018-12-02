using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Meteor : MonoBehaviour
{
    [Header("====================Drops====================")]
    [SerializeField] private GameObject _drop1;
    [SerializeField] private GameObject _drop2;
    [SerializeField] private GameObject _drop3;

    [Header("====================Drop rates====================")]
    [SerializeField] [Range(0, 1)] private float _drop1Chance;
    [SerializeField] [Range(0, 1)] private float _drop2Chance;
    [SerializeField] [Range(0, 1)] private float _drop3Chance;

    [Header("====================Meteor Settings====================")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _speed;

    private int _health;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = transform.up * _speed;

        _health = _maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag(EnvironmentVariables.BulletTag))
        {
            ModifyHealth(-1);
            // Play animation and sound
        }
    }

    private void ModifyHealth(int delta)
    {
        _health += delta;

        if (_health <= 0)
        {
            SpawnDrops();
            Destroy(gameObject);
        }
    }

    private void SpawnDrops()
    {
        var drop1Result = Random.Range(1, (int) (1 / _drop1Chance) + 1);
        if (drop1Result == 1)
        {
            Instantiate(_drop1, transform.position, transform.rotation);
        }

        var drop2Result = Random.Range(1, (int)(1 / _drop2Chance) + 1);
        if (drop2Result == 1)
        {
            Instantiate(_drop2, transform.position, transform.rotation);
        }

        var drop3Result = Random.Range(1, (int)(1 / _drop3Chance) + 1);
        if (drop3Result == 1)
        {
            Instantiate(_drop3, transform.position, transform.rotation);
        }
    }
}
