using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetBehavior : MonoBehaviour
{
    [SerializeField]
    private float _resetThreshold = -5.0f;

    private Vector3 _resetPosition;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        if (TryGetComponent(out Rigidbody rigidbody))
            _rigidbody = rigidbody;
    }

    void Start()
    {
        _resetPosition = gameObject.transform.position;
    }

    void Update()
    {
        if (gameObject.transform.position.y < _resetThreshold)
        {
            gameObject.transform.position = _resetPosition;
            if (_rigidbody)
                _rigidbody.velocity = new Vector3();
        }
    }
}
