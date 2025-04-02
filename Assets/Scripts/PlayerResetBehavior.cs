using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerResetBehavior : MonoBehaviour
{
    [SerializeField]
    private float _resetThreshold = -5.0f;

    private Vector3 _resetPosition;

    private Rigidbody _rigidbody;

    public UnityEvent OnReset;

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
        // if the object has fallen below the threshold, put it back where it was and remove its velocity (if it has any)
        if (gameObject.transform.position.y < _resetThreshold)
        {
            gameObject.transform.position = _resetPosition;
            OnReset.Invoke();

            if (_rigidbody)
                _rigidbody.velocity = new Vector3();
        }
    }
}
