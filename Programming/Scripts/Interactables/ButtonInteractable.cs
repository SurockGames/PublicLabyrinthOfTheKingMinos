using UnityEngine;
using UnityEngine.Events;

public class ButtonInteractable : Interactable
{
    [SerializeField] private UnityEvent OnInteractEvent;
    public override void Interact(GameObject gameObject)
    {
        OnInteractEvent.Invoke();
    }
}
