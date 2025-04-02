using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDisableBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject _meshObject;

    private bool _isDisabled = false;

    public UnityEvent OnPlayerDisable;

    public void Disable()
    {
        if (_isDisabled) return;

        OnPlayerDisable.Invoke();

        if (gameObject.TryGetComponent(out PlayerController controller))
            controller.enabled = false;
        else
            Debug.LogWarning("PlayerDisableBehavior: No PlayerController to disable.");

        if (_meshObject.TryGetComponent(out Renderer renderer))
            renderer.enabled = false;
        else
            Debug.LogWarning("PlayerDisableBehavior: No Renderer to disable.");

        if (gameObject.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.isKinematic = true;
        else
            Debug.LogWarning("PlayerDisableBehavior: No Rigidbody to make kinematic.");
    }
}
