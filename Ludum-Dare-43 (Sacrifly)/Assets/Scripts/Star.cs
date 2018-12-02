using UnityEngine;

public class Star : MonoBehaviour
{
    private readonly FloatRange _rotateSpeedRange = new FloatRange(0, 1);
    private readonly FloatRange _scaleRange = new FloatRange(0.01f, 0.05f);
    private readonly FloatRange _movementSpeedRange = new FloatRange(0.01f, 0.05f);
    private readonly FloatRange _xPositionRange = new FloatRange(-9, 4.5f);
    private readonly FloatRange _yPositionRange = new FloatRange(-5, 5);

    private float _movementSpeed;
    private float _rotateSpeed;

    private void Start()
    {
        _movementSpeed = Random.Range(_movementSpeedRange.Min, _movementSpeedRange.Max);
        _rotateSpeed = Random.Range(_rotateSpeedRange.Min, _rotateSpeedRange.Max);

        var randomScale = Random.Range(_scaleRange.Min, _scaleRange.Max);
        transform.localScale = new Vector3(randomScale, randomScale, 1);

        transform.position = new Vector3(Random.Range(_xPositionRange.Min, _xPositionRange.Max),
            Random.Range(_yPositionRange.Min, _yPositionRange.Max), 0);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * _movementSpeed * Time.deltaTime);
        transform.Rotate(0, 0, _rotateSpeed);

        // Instead of teleporting them you can probably do some cool animation to make them reappear somewhere
        if (transform.position.x > _xPositionRange.Max + 0.5f)
        {
            transform.position = new Vector3(_xPositionRange.Min, transform.position.y, 0);
        }
        else if (transform.position.x < _xPositionRange.Min - 0.5f)
        {
            transform.position = new Vector3(_xPositionRange.Max, transform.position.y, 0);
        }
        else if (transform.position.y > _yPositionRange.Max + 0.5f)
        {
            transform.position = new Vector3(transform.position.x, _yPositionRange.Min, 0);
        }
        else if (transform.position.y < _yPositionRange.Min - 0.5f)
        {
            transform.position = new Vector3(transform.position.x, _yPositionRange.Max, 0);
        }
    }
}
