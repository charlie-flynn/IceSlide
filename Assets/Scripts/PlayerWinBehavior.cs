using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWinBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject _meshObject;

    private bool _isDisabled = false;

    public UnityEvent OnPlayerWin;

    public void Win()
    {
        if (_isDisabled) return;

        OnPlayerWin.Invoke();

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

        if (gameObject.TryGetComponent(out PlayerTimerBehavior timer))
            timer.enabled = false;
        else
            Debug.LogWarning("PlayerDisableBehavior: No PlayerTimerBehavior to disable.");
    }
}
