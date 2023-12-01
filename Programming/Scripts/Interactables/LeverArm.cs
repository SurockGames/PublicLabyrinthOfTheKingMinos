using UnityEngine;
using UnityEngine.Events;

public class LeverArm : ButtonInteractable
{
    [SerializeField] private UnityEvent OnTurnOn;
    [SerializeField] private UnityEvent OnTurnOff;
    private bool isOn;

    public override void Interact(GameObject gameObject)
    {
        if (isOn)
            OnTurnOff?.Invoke();
        else
            OnTurnOn?.Invoke();

        isOn = !isOn;

        base.Interact(gameObject);

    }
}
