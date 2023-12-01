using SurockGames;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Box : Interactable, ICarriable, IPickUpable
{
    private Rigidbody rb;
    public bool CanBePickedUp => canBeCarried;

    private bool canBeCarried = true;
    private Vector3 localPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Interact(GameObject gameObject)
    {

    }

    public bool TryCarring(GameObject interactionObject, out Rigidbody carriable)
    {
        carriable = rb;

        if (interactionObject.TryGetComponent(out PlayerCharacterInteraction character))
        {
            Drag(character);

            canBeCarried = false;
            return true;
        }
        return false;
    }

    private void Drag(PlayerCharacterInteraction character)
    {
        Debug.Log("Drag - " + gameObject.name + " By - " + character.name);

    }

    public bool TryPickUp(GameObject interactionObject, Transform carringPoint, out Rigidbody pickUpable)
    {
        pickUpable = rb;

        if (interactionObject.TryGetComponent(out PlayerCharacterInteraction character))
        {
            PickUp(character, carringPoint);

            canBeCarried = false;
            return true;
        }
        return false;
    }

    private void PickUp(PlayerCharacterInteraction character, Transform carringPoint)
    {
        Debug.Log("Pick - " + gameObject.name + " By - " + character.name);

        rb.useGravity = false;
        transform.SetParent(carringPoint, true);
        transform.localPosition = Vector3.zero;
        //transform.parent = carringPoint;
    }

    public void Drop(Vector3 dropPos)
    {
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.freezeRotation = true;

        transform.parent = null;
        transform.position = dropPos;
        //transform.position = Vector3.zero;
    }

    public void Throw(Vector3 forceVector)
    {
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.freezeRotation = true;

        transform.SetParent(null, true);
        rb.AddForce(forceVector, ForceMode.Impulse);
        transform.parent = null;
    }

}
