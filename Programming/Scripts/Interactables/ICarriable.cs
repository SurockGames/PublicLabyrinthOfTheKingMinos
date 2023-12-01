using SurockGames;
using UnityEngine;

public interface ICarriable 
{
    public bool TryCarring(GameObject interactionObject, out Rigidbody carriable);
    public bool CanBePickedUp { get; }
    public SizeEnum Size { get; }
}
