using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;

    [SerializeField] private UnityEvent OnMoveEvent;

    public void MoveToFirstPoint(float speed)
    {
        transform.DOMove(firstPoint.position, speed);
        OnMoveEvent?.Invoke();
    }

    public void MoveToSecondPoint(float speed)
    {
        transform.DOMove(secondPoint.position, speed);
        OnMoveEvent?.Invoke();
    }

    public void MoveToGivvenPoint(Transform pointToMove, float speed)
    {
        transform.DOMove(pointToMove.position, speed);
        OnMoveEvent?.Invoke();
    }
}
