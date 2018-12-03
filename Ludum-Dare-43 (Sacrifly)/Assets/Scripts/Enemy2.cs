using UnityEngine;

public class Enemy2 : Enemy
{
    private Transform _player;

    private void Start()
    {
        _player = FindObjectOfType<ShipController>().transform;
    }

    protected override void Update()
    {
        Rigidbody.velocity = (_player.position - transform.position).normalized * _movementSpeed;
    }

}
