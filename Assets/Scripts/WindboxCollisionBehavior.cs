using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindboxCollisionBehavior : MonoBehaviour
{
    [SerializeField]
    private float _windForce;

    private void OnTriggerStay(Collider other)
    {
        

        if (other.gameObject.GetComponentInParent<Rigidbody>())
        {
            // apply force
            other.gameObject.GetComponentInParent<Rigidbody>().AddForce(transform.forward * _windForce * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}
