using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _maxRegularSpeed = 25;

    [SerializeField]
    private float _maxIceSpeed = 100;

    [SerializeField]
    private float _regularRotationSpeed = 60;

    [SerializeField]
    private float _iceRotationSpeed = 30;

    [SerializeField]
    private float _speed = 20;

    [SerializeField]
    private float _regularDecceleration = 1;

    [SerializeField]
    private float _iceDecceleration = -0.5f;

    private float _decceleration;

    private float _rotationSpeed;

    private float _maxSpeed;

    private Rigidbody _rigidbody;

    private Material _materialStandingOn;

    private Vector3 _playerInput;

    // Start is called before the first frame update
    void Start()
    {
        _rotationSpeed = _regularRotationSpeed;
        _decceleration = _regularDecceleration;

        if (gameObject.GetComponent<Rigidbody>())
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        else
            Debug.LogError("PlayerController: Attached object has no Rigidbody component! Most of this script will not work without one!");

    }

    private void Update()
    {
        _playerInput.x = Input.GetAxisRaw("Horizontal");
        _playerInput.z = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (!_rigidbody) return;

        float movementForce = _playerInput.z * _speed * Time.deltaTime;
        float rotationForce = _playerInput.x * _rotationSpeed * Time.deltaTime;

        Vector3 newRotation = new Vector3();
        newRotation.y = rotationForce;
        gameObject.transform.Rotate(newRotation);

        _rigidbody.AddForce(_rigidbody.transform.forward * movementForce, ForceMode.VelocityChange);

        if (movementForce == 0)
        {
            Vector3 deccelerationForce = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z) * _decceleration * -1;
            _rigidbody.AddForce(deccelerationForce, ForceMode.Acceleration);
        }

        Vector3 newVelocity = _rigidbody.velocity;
        Mathf.Clamp(newVelocity.z, -_maxSpeed, _maxSpeed);
        _rigidbody.velocity = newVelocity;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Renderer>())
            _materialStandingOn = other.gameObject.GetComponent<Renderer>().material;

        if (_materialStandingOn.name.StartsWith("IceMat"))
        {
            _maxSpeed = _maxIceSpeed;
            _rotationSpeed = _iceRotationSpeed;
            _decceleration = _iceDecceleration;
        }
        else
        {
            _maxSpeed = _maxRegularSpeed;
            _rotationSpeed = _regularRotationSpeed;
            _decceleration = _regularDecceleration;
        }
    }
}
