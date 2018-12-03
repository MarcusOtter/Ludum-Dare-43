using UnityEngine;

public class Enemy1 : Enemy
{
    [SerializeField] private bool _enemy3;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _rotateTransform;

    protected override void Update()
    {
        base.Update();

        if (!_enemy3) { return; }
        _rotateTransform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
    }
}
