using UnityEngine;

namespace SurockGames
{
    public interface IPickUpable
    {
        public bool CanBePickedUp { get; }
        public SizeEnum Size { get; }
        public bool TryPickUp(GameObject interactionObject, Transform carringPoint, out Rigidbody pickUpable);
        public void Drop(Vector3 dropPointPosition);
        public void Throw(Vector3 forceVector);
    }
}