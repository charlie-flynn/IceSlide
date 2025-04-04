using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _regularAcceleration = 30.0f;

    [SerializeField]
    private float _iceAcceleration = 40.0f;

    [SerializeField]
    private float _regularDecceleration = 60.0f;

    [SerializeField]
    private float _iceDecceleration = 5.0f;

    [SerializeField]
    private float _regularMaxSpeed = 20.0f;

    [SerializeField]
    private float _iceMaxSpeed = 100.0f;

    private float _acceleration;

    private float _decceleration;

    private float _maxSpeed;

    private Rigidbody _rigidbody;

    private Material _materialStandingOn;

    private Vector3 _playerInput;

    private bool _onGround;

    private void Awake()
    {
        if (gameObject.GetComponent<Rigidbody>())
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        else
            Debug.LogError("PlayerController: Attached object has no Rigidbody component! Most of this script will not work without one!");
    }

    // Start is called before the first frame update
    void Start()
    {
        _acceleration = _regularAcceleration;
        _decceleration = _regularDecceleration;
        _maxSpeed = _regularMaxSpeed;
        _onGround = true;
    }

    private void Update()
    {
        // take in player input
        _playerInput.x = Input.GetAxisRaw("Horizontal");
        _playerInput.z = Input.GetAxisRaw("Vertical");

        // if y velocity is goin up and the player is not on the ground, remove the upward velocity
        Vector3 newVelocity = _rigidbody.velocity;
        if (!_onGround && newVelocity.y > 0.0f)
        {
            newVelocity.y = 0.0f;
            _rigidbody.velocity = newVelocity;
        }
    }

    private void FixedUpdate()
    {
        // if no rigidbody, return
        if (!_rigidbody) return;

        // calculate the movement force based on speed values and delta time
        Vector3 movementForce = _playerInput * _acceleration * Time.deltaTime;

        // use doubled decceleration if moving away from the current velocity
        if (movementForce != new Vector3() && Vector3.Angle(movementForce, _rigidbody.velocity) > 160)
            movementForce = _playerInput * _decceleration * 2 * Time.deltaTime;

        // clamps the velocity to the given max speed value
        if (_rigidbody.velocity.magnitude > _maxSpeed)
        {
            Vector3 deccelerateForce = _rigidbody.velocity;

            deccelerateForce.y = 0.0f;

            if (deccelerateForce.x > _maxSpeed)
                deccelerateForce.x = (deccelerateForce.x - _maxSpeed) * -1;
            else if (deccelerateForce.x < -_maxSpeed)
                deccelerateForce.x = Mathf.Abs(deccelerateForce.x + _maxSpeed);
            else
                deccelerateForce.x = 0.0f;

            if (deccelerateForce.z > _maxSpeed)
                deccelerateForce.z = (deccelerateForce.z - _maxSpeed) * -1;
            else if (deccelerateForce.z < -_maxSpeed)
                deccelerateForce.z = Mathf.Abs(deccelerateForce.z + _maxSpeed);
            else
                deccelerateForce.z = 0.0f;

            _rigidbody.AddForce(deccelerateForce, ForceMode.VelocityChange);
        }

        // add the force
        _rigidbody.AddForce(movementForce, ForceMode.VelocityChange);

        // if there is no movementForce, deccelerate based on decceleration value and delta time
        if (movementForce.x == 0)
        {
            Vector3 deccelerationForce = new Vector3(_rigidbody.velocity.x, 0, 0) * _decceleration * -1 * Time.deltaTime;
            _rigidbody.AddForce(deccelerationForce, ForceMode.Acceleration);
        }
        if (movementForce.z == 0)
        {
            Vector3 deccelerationForce = new Vector3(0, 0, _rigidbody.velocity.z) * _decceleration * -1 * Time.deltaTime;
            _rigidbody.AddForce(deccelerationForce, ForceMode.Acceleration);
        }


        /*
        if (movementForce == new Vector3())
        {
            Vector3 deccelerationForce = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z) * _decceleration * -1 * Time.deltaTime;
            _rigidbody.AddForce(deccelerationForce, ForceMode.Acceleration);
        }
         */



        // some old code from when i was tryna do tank controls
        /*
        float movementForce = _playerInput.z * _speed * Time.deltaTime;
        float rotationForce = _playerInput.x * _rotationSpeed * Time.deltaTime;

        // rotate
        Vector3 newRotation = new Vector3();
        newRotation.y = rotationForce;
        gameObject.transform.Rotate(newRotation);

        // move
        _rigidbody.AddForce(_rigidbody.transform.forward * movementForce, ForceMode.VelocityChange);
         */
    }

    private void OnTriggerEnter(Collider other) { _onGround = true; }
    private void OnTriggerExit(Collider other) { _onGround = false; }

    private void OnTriggerStay(Collider other)
    {
        _onGround = true;

        // get the material this is standing on
        if (other.gameObject.GetComponent<Renderer>())
            _materialStandingOn = other.gameObject.GetComponent<Renderer>().material;


        // if the material this is standing on is ice, set values to their ice version
        // blEck. sorry im allergic to string comparison. unless this function doesnt do that
        if (_materialStandingOn.name.StartsWith("Ice"))
        {
            _acceleration = _iceAcceleration;
            _decceleration = _iceDecceleration;
            _maxSpeed = _iceMaxSpeed;
        }
        else
        {
            _acceleration = _regularAcceleration;
            _decceleration = _regularDecceleration;
            _maxSpeed = _regularMaxSpeed;
        }
    }
}
