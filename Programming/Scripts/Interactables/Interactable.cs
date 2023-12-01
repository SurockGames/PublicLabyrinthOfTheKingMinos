using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected SizeEnum size;
    public SizeEnum Size => size;
    public abstract void Interact(GameObject gameObject);
}
