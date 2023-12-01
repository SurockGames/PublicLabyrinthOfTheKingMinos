using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiddleWall : MonoBehaviour
{
    [SerializeField] protected List<GameObject> signalablesToActivate;
    [SerializeField] protected int amountOfSignalsToWork;
    [SerializeField] protected UnityEvent OnAllSignalsEvent;
    [SerializeField] protected UnityEvent OnTurnOffSignalEvent;

    public  List<GameObject> signalablesActivated = new List<GameObject>();
    public  int prevAmountActivated;
    public int signalsTurned;
    public int previosAmountOfSignals;

    public void TurnOnOneSignalPassObject(GameObject signalable)
    {
        if (CheckIfNeeded(signalable))
        {
            signalablesActivated.Add(signalable);
            CheckIfAllNeededIsActivated();

            prevAmountActivated++;
        }

    }

    public void TurnOffOneSignalPassObject(GameObject signalable)
    {
        if (CheckIfNeeded(signalable))
        {
            signalablesActivated.Remove(signalable);
            CheckIfAllNeededIsActivated();

            prevAmountActivated--;
        }

    }

    private bool CheckIfNeeded(GameObject signalable)
    {
        foreach (var sign in signalablesToActivate)
        {
            if (signalable == sign)
            {
                return true;
            }
        }
        return false;
    }
    public virtual void TurnOnOneSignal()
    {
        previosAmountOfSignals = signalsTurned;
        signalsTurned++;
        CheckIfAllNeededIsActivated();
    }

    public virtual void TurnOffOneSignal()
    {
        previosAmountOfSignals = signalsTurned;
        signalsTurned--;
        CheckIfAllNeededIsActivated();
    }

    protected virtual bool CheckIfAllTurned()
    {
        if (signalsTurned >= amountOfSignalsToWork)
        {
            return true;
        }
        else if (signalsTurned < amountOfSignalsToWork && previosAmountOfSignals == amountOfSignalsToWork)
        {
            return false;
        }
        return false;
    }
    protected void CheckIfAllNeededIsActivated()
    {
        if (signalablesActivated.Count == signalablesToActivate.Count)
        {
            if (CheckIfAllTurned())
                OnAllSignalsEvent?.Invoke();
            else if (signalsTurned < amountOfSignalsToWork && previosAmountOfSignals == amountOfSignalsToWork)
                OnTurnOffSignalEvent?.Invoke();

        }
        else if (signalablesActivated.Count < signalablesToActivate.Count && prevAmountActivated == signalablesToActivate.Count)
        {
            OnTurnOffSignalEvent?.Invoke();
        }
    }
}
