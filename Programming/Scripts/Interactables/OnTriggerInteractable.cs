using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OnTriggerInteractable : MonoBehaviour
{
    [SerializeField] protected UnityEvent OnTriggerEnterEvent;
    [SerializeField] protected UnityEvent OnTriggerExitEvent;

    protected virtual void OnTriggerEnter(Collider collider)
    {
        OnTriggerEnterEvent?.Invoke();
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        OnTriggerExitEvent?.Invoke();
    }
}
