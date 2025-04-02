using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerResetBehavior : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The amount the Y value must be below in order to be automatically reset.")]
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
        if (gameObject.transform.position.y < _resetThreshold)
            Restart();
    }

    // had to name this restart because unity already had a reset function
    public void Restart()
    {
        // put the gameObject back where it was and remove its velocity (if it has a rigidbody)
        gameObject.transform.position = _resetPosition;

        if (_rigidbody)
            _rigidbody.velocity = new Vector3();

        OnReset.Invoke();
    }
}
