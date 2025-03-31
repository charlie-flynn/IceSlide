using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeathBehavior : MonoBehaviour
{
    private bool _isDead = false;

    public UnityEvent OnPlayerDeath;

    public void Die()
    {
        if (_isDead) return;

        OnPlayerDeath.Invoke();

        if (gameObject.TryGetComponent(out PlayerController controller))
            controller.enabled = false;
        else
            Debug.LogWarning("PlayerDeathBehavior: No PlayerController to disable.");

        if (gameObject.TryGetComponent(out Renderer renderer))
            renderer.enabled = false;
        else
            Debug.LogWarning("PlayerDeathBehavior: No Renderer to disable.");

        if (gameObject.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.isKinematic = true;
        else
            Debug.LogWarning("PlayerDeathBehavior: No Rigidbody to make kinematic.");
    }
}
