using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour
{
    [SerializeField] protected SizeEnum size;
    [SerializeField] private UnityEvent OnPushEvent;
    [SerializeField] private UnityEvent OnUnPushEvent;
    private List<GameObject> collisions = new List<GameObject>();
    private bool isPushed;

    public SizeEnum Size => size;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Character character))
        {
            if (character.Size >= Size)
            {
                collisions.Add(collider.gameObject);

                if (!isPushed)
                    Push();
            }
        }
        else if (collider.TryGetComponent(out Interactable interactable))
        {
            if (interactable.Size >= Size)
            {
                collisions.Add(collider.gameObject);

                if (!isPushed)
                    Push();
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        foreach (GameObject obj in collisions)
        {
            if (obj == collider.gameObject)
            {
                collisions.Remove(obj);

                if (!IsThereAnyObjectOver())
                {
                    UnPush();
                }

                break;
            }
        }
    }

    private bool IsThereAnyObjectOver()
    {
        if (collisions.Count > 0)
        {
            return true;
        }

        return false;
    }

    private void Push()
    {
        isPushed = true;
        OnPushEvent?.Invoke();
    }

    private void UnPush()
    {
        isPushed = false;
        OnUnPushEvent?.Invoke();
    }
}
