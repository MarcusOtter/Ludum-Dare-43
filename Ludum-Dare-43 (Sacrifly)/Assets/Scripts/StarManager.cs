using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;

    private readonly List<Transform> _children = new List<Transform>();

    private void Start ()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _children.Add(transform.GetChild(i));
        }

        foreach (var child in _children)
        {
            child.position = new Vector3(Random.Range(_minX, _maxX), Random.Range(_minY, _maxY), 0);
        }
	}

}
