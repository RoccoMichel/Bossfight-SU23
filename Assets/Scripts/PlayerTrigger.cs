using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour
{
    public bool oneShot;
    private bool triggered;
    public UnityEvent onTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (oneShot && triggered) return;

        triggered = true;
        onTrigger.Invoke();
    }
}