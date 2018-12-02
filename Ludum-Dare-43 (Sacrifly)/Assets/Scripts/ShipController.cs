using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 3;
    [SerializeField] private float _turningSpeed = 3;

    private Rigidbody2D _rigidbody;

    private void OnEnable()
    {
        GameSetting.OnValueChanged += HandleChangedSetting;
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

    private void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate ()
	{
        transform.Rotate(0, 0, PlayerInput.HorizontalInput * _turningSpeed * -1);
	    _rigidbody.velocity = transform.up * _movementSpeed;
	}

    private void OnDisable()
    {
        GameSetting.OnValueChanged -= HandleChangedSetting;
    }
}
