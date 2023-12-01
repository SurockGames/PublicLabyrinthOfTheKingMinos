using KinematicCharacterController.Walkthrough.ClimbingLadders;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Finish : OnTriggerInteractable
{
    [SerializeField] private UnityEvent OnFinishEvent;

    private bool isTeseusNeeded;
    private bool isMinotaurNeeded;
    private bool isTalosNeeded;

    private bool isTeseusOnFinish;
    private bool isMinotaurOnFinish;
    private bool isTalosOnFinish;

    private void Start()
    {
        if (Player.Teseus != null)
            isTeseusNeeded = true;
        else if (Player.Minotaur != null)
            isMinotaurNeeded = true;
        else if (Player.Talos != null)
            isTalosNeeded = true;
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Teseus teseus))
        {
            isTeseusOnFinish = true;
        }
        if (collider.TryGetComponent(out Minotaur minotaur))
        {
            isMinotaurOnFinish = true;
        }
        if (collider.TryGetComponent(out Talos talos))
        {
            isTalosOnFinish = true;
        }

        CheckIfAllOnFinish();
        base.OnTriggerEnter(collider);
    }

    protected override void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Teseus teseus))
        {
            isTeseusOnFinish = false;
        }
        if (collider.TryGetComponent(out Minotaur minotaur))
        {
            isMinotaurOnFinish = false;
        }
        if (collider.TryGetComponent(out Talos talos))
        {
            isTalosOnFinish = false;
        }

        base.OnTriggerExit(collider);
    }

    private void CheckIfAllOnFinish()
    {
        if (isTeseusNeeded && !isTeseusOnFinish) return;
        if (isMinotaurNeeded && !isMinotaurOnFinish) return;
        if (isTalosNeeded && !isTalosOnFinish) return;

        FinishLevel();
    }

    private void FinishLevel()
    {
        OnFinishEvent?.Invoke();
    }
}
