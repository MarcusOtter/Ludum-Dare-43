using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private Vector2 _rotateSpeedMinMax = new Vector2(0, 1);
    [SerializeField] private Vector2 _scaleMinMax = new Vector2(1, 5);
    [SerializeField] private Vector2 _movementSpeedMinMax = new Vector2(0.01f, 0.05f);
    [SerializeField] private Vector2 _xPositionMinMax = new Vector2(-9, 4.5f);
    [SerializeField] private Vector2 _yPositionMinMax = new Vector2(-5, 5);

    private float _movementSpeed;
    private float _rotateSpeed;

    private void Start()
    {
        _movementSpeed = Random.Range(_movementSpeedMinMax.x, _movementSpeedMinMax.y);
        _rotateSpeed = Random.Range(_rotateSpeedMinMax.x, _rotateSpeedMinMax.y);

        var randomScale = Random.Range(_scaleMinMax.x, _scaleMinMax.y);
        transform.localScale = new Vector3(randomScale, randomScale, 1);

        transform.position = new Vector3(Random.Range(_xPositionMinMax.x, _xPositionMinMax.y),
            Random.Range(_yPositionMinMax.x, _yPositionMinMax.y), 0);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * _movementSpeed * Time.deltaTime);
        transform.Rotate(0, 0, _rotateSpeed);

        // Instead of teleporting them you can probably do some cool animation to make them reappear somewhere
        if (transform.position.x > _xPositionMinMax.y + 0.5f)
        {
            transform.position = new Vector3(_xPositionMinMax.x, transform.position.y, 0);
        }
        else if (transform.position.x < _xPositionMinMax.x - 0.5f)
        {
            transform.position = new Vector3(_xPositionMinMax.y, transform.position.y, 0);
        }
        else if (transform.position.y > _yPositionMinMax.y + 0.5f)
        {
            transform.position = new Vector3(transform.position.x, _yPositionMinMax.x, 0);
        }
        else if (transform.position.y < _yPositionMinMax.x - 0.5f)
        {
            transform.position = new Vector3(transform.position.x, _yPositionMinMax.y, 0);
        }
    }
}
