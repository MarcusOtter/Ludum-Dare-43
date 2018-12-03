using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 3;
    [SerializeField] private float _turningSpeed = 3;

    private Rigidbody2D _rigidbody;
    internal Sacrificer Sacrificer { get; private set; }

    private void OnEnable()
    {
        GameSetting.OnValueChanged += HandleChangedSetting;
    }

    private void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody2D>();
	    Sacrificer = FindObjectOfType<Sacrificer>();
	}

	private void FixedUpdate ()
	{
        transform.Rotate(0, 0, PlayerInput.HorizontalInput * _turningSpeed * -1);
	    _rigidbody.velocity = transform.up * _movementSpeed;
	}

    private void HandleChangedSetting(object sender, EventArgs e)
    {
        var changedSetting = (GameSetting) sender;

        switch (changedSetting.SettingType)
        {
            case GameSettingType.MovementSpeed:
                _movementSpeed = changedSetting.Value;
                break;

            case GameSettingType.TurningSpeed:
                _turningSpeed = changedSetting.Value;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag(EnvironmentVariables.BulletTag))
        {
            var isEnemyBullet = col.collider.GetComponent<Bullet>().IsEnemyBullet;

            if (isEnemyBullet)
            {
                Sacrificer.ModifyHitpoints(-1);
            }
        }
        else if (col.collider.CompareTag(EnvironmentVariables.EnemyTag))
        {
            Sacrificer.ModifyHitpoints(-1);
            Destroy(col.gameObject);
        }
    }

    private void OnDisable()
    {
        GameSetting.OnValueChanged -= HandleChangedSetting;
    }
}
