using UnityEngine;
using UnityEngine.Events;

public class CheckForMultipleSignals : MonoBehaviour
{
    [SerializeField] protected int amountOfSignalsToWork;

    [SerializeField] protected UnityEvent OnAllSignalsEvent;
    [SerializeField] protected UnityEvent OnTurnOffSignalEvent;

    protected int signalsTurned;
    protected int previosAmountOfSignals;

    public virtual void TurnOnOneSignal()
    {
        previosAmountOfSignals = signalsTurned;
        signalsTurned++;
        CheckIfAllTurned();
    }

    public virtual void TurnOffOneSignal()
    {
        previosAmountOfSignals = signalsTurned;
        signalsTurned--;
        CheckIfAllTurned();
    }

    protected virtual void CheckIfAllTurned()
    {
        if (signalsTurned >= amountOfSignalsToWork)
        {
            OnAllSignalsEvent?.Invoke();
        }
        else if (signalsTurned < amountOfSignalsToWork && previosAmountOfSignals == amountOfSignalsToWork)
        {
            OnTurnOffSignalEvent?.Invoke();
        }
    }
}
