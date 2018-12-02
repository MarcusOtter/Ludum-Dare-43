using System;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [Header("====================Fire setting====================")]
    [SerializeField] private bool _isAutomatic = true;
    [Space(-1)]
    [Header("OR")]
    [Space(-11)]
    [SerializeField] private KeyCode _manualFireKey;

    [Header("====================Shoot delay====================")]
    [SerializeField] private bool _isPlayerShooter;
    [SerializeField] private float _shootDelayMax = 0.3f;

    [Header("====================References====================")]
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Bullet _bulletPrefab;

    private float _lastFireTime;

    private void OnEnable()
    {
        if (!_isPlayerShooter) { return; }

        GameSetting.OnValueChanged += HandleChangedSetting;
    }

    private void HandleChangedSetting(object sender, EventArgs e)
    {
        var changedSetting = (GameSetting) sender;

        if (changedSetting.SettingType != GameSettingType.ShootDelay) { return; }
        _shootDelayMax = changedSetting.Value;
    }

    private void Update()
    {
        if (_isAutomatic)
        {
            CheckAutomaticShoot();
        }
        else
        {
            CheckManualShoot();
        }
    }

    private void CheckManualShoot()
    {
        if (Input.GetKeyDown(_manualFireKey) && Time.time > _lastFireTime + _shootDelayMax)
        {
            Shoot();
        }
    }

    private void CheckAutomaticShoot()
    {
        if (Time.time > _lastFireTime + _shootDelayMax)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            var bullet = Instantiate(_bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), transform.root.GetComponent<Collider2D>());
            bullet.Shoot(!_isPlayerShooter);
        }

        _lastFireTime = Time.time;
    }

    private void OnDisable()
    {
        GameSetting.OnValueChanged -= HandleChangedSetting;
    }
}
