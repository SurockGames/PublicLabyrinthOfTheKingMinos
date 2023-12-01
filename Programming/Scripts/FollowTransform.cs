using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] protected Transform targetToFollow;

    private Transform trs;

    protected virtual void Awake()
    {
        trs = GetComponent<Transform>();
    }

    protected virtual void LateUpdate()
    {
        FollowTarget();
    }

    protected virtual void FollowTarget()
    {
        trs.position = targetToFollow.position;
    }
}
