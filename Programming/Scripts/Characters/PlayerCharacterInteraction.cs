using Sirenix.OdinInspector;
using SurockGames;
using SurockGames.Characters;
using SurockGames.System;
using System;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = KinematicCharacterController.CharacterController;

[RequireComponent(typeof(Character))]
public class PlayerCharacterInteraction : SerializedMonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private Transform carryPoint;
    public Transform CarryPoint => carryPoint;

    [Header("Settings")]
    [SerializeField] private Vector3 rayCastOffset;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float radius;
    [SerializeField] private Dictionary<SizeDifferenceEnum, int> movementSpeedReductionPercentWithObj;
    public SizeEnum Size => GetComponent<Character>().Size;


    [Header("Throw Settings")]
    [SerializeField] private float throwStrength;
    [SerializeField] private LayerMask throwCollisionMask;

    [Header("Aim Display Controls")]
    [SerializeField, Range(10, 100)] private int LinePoints = 25;
    [SerializeField, Range(0.01f, 0.25f)] private float TimeBetweenPoints = 0.1f;

    protected Transform trs;
    private Rigidbody interactionObjectRB;
    private CharacterController characterController;
    private bool isCarringObject;
    public bool isActive;
    private bool isAiming;
    private Rigidbody carriedObject;
    private CarringTypesEnum carringType;

    protected void Awake()
    {
        trs = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Game.InputService.OnInteractEvent += TryToInteract;
        Game.InputService.OnThrowEvent += TryThrow;

        ActiveCharacterSetter.OnCharacterChangedEvent += CheckIfActive;
    }

    private void OnDisable()
    {
        Game.InputService.OnInteractEvent -= TryToInteract;
        Game.InputService.OnThrowEvent -= TryThrow;

        ActiveCharacterSetter.OnCharacterChangedEvent -= CheckIfActive;
    }

    private void Update()
    {
        CheckIfNeedToAim();
    }

    private void CheckIfActive(Character character)
    {
        if (character.gameObject == gameObject)
            isActive = true;
        else
            isActive = false;
    }

    protected void TryToInteract()
    {
        if (!isActive) return;

        if (!isCarringObject)
        {
            TryGetForwardInteractable(out Interactable interactable);
            if (interactable == null) return;

            if (interactable.Size == Size)
            {
                if (interactable.TryGetComponent(out IPickUpable iPickUpable))
                {
                    if (iPickUpable.TryPickUp(gameObject, interactionPoint, out interactionObjectRB))
                    {
                        isCarringObject = true;
                        carringType = CarringTypesEnum.PickingUp;
                        StartMovingWithObject(SizeDifferenceEnum.Egual, interactionObjectRB, CarringTypesEnum.PickingUp);
                    }
                }
                else
                {
                    interactable.Interact(gameObject);
                }
            }
            else if (interactable.Size == Size + 1)
            {
                if (interactable.TryGetComponent(out ICarriable iCarriable))
                {
                    if (iCarriable.TryCarring(gameObject, out interactionObjectRB))
                    {
                        isCarringObject = true;
                        carringType = CarringTypesEnum.Dragging;
                        StartMovingWithObject(SizeDifferenceEnum.Bigger, interactionObjectRB, CarringTypesEnum.Dragging);
                    }
                }
                else
                {
                    interactable.Interact(gameObject);
                }
            }
        }
        else
        {
            DropObject();
        }
    }

    protected void TryGetForwardInteractable(out Interactable interactable)
    {
        Vector3 localOffset = trs.TransformDirection(rayCastOffset);


        if (Physics.SphereCast(trs.position + localOffset, radius, trs.forward, out RaycastHit hit, maxDistance, layerMask))
        {
            if (hit.collider.TryGetComponent(out Interactable _interactable))
            {
                interactable = _interactable;
            }
            else
            {
                interactable = null;
            }
        }
        else
        {
            interactable = null;
        }
    }


    private void OnDrawGizmos()
    {
        Vector3 localOffset = transform.TransformDirection(rayCastOffset);

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position + localOffset, transform.forward * maxDistance);
        Gizmos.DrawWireSphere(transform.position + localOffset + transform.forward * maxDistance, radius);
    }

    private void DropObject()
    {
        isCarringObject = false;

        StopMovingWithObject();
    }

    private void CheckIfNeedToAim()
    {
        if (!isCarringObject || !isActive || carringType == CarringTypesEnum.Dragging)
        {
            lineRenderer.enabled = false;
            isAiming = false;
            return;
        }

        isAiming = Game.InputService.IsAiming;

        if (isAiming)
        {
            DrawProjection();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void DrawProjection()
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = Mathf.CeilToInt(LinePoints / TimeBetweenPoints) + 1;
        Vector3 startPosition = carryPoint.position;
        Vector3 startVelocity = throwStrength * carryPoint.forward / carriedObject.mass;
        int i = 0;
        lineRenderer.SetPosition(i, startPosition);
        for (float time = 0; time < LinePoints; time += TimeBetweenPoints)
        {
            i++;
            Vector3 point = startPosition + time * startVelocity;
            point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);

            lineRenderer.SetPosition(i, point);

            Vector3 lastPosition = lineRenderer.GetPosition(i - 1);

            if (Physics.Raycast(lastPosition,
                (point - lastPosition).normalized,
                out RaycastHit hit,
                (point - lastPosition).magnitude,
                throwCollisionMask))
            {
                lineRenderer.SetPosition(i, hit.point);
                lineRenderer.positionCount = i + 1;
                return;
            }
        }
    }

    private void TryThrow()
    {
        if (isAiming)
        {
            lineRenderer.enabled = false;
            carriedObject.GetComponent<IPickUpable>().Throw(throwStrength * carryPoint.forward);
            isCarringObject = false;

            characterController.StopCarringObject();
        }
    }

    public void StartMovingWithObject(SizeDifferenceEnum sizeDifference, Rigidbody carriable, CarringTypesEnum carringType)
    {
        interactionPoint.position = trs.position;

        if (carringType == CarringTypesEnum.PickingUp)
        {
            interactionPoint.position = carryPoint.position;
            interactionPoint.parent = trs;
            carriedObject = carriable;

            characterController.StartCarringObject(carriable, interactionPoint, carringType);
        }
        else if (carringType == CarringTypesEnum.Dragging)
        {
            interactionPoint.parent = carriable.gameObject.transform;
            characterController.StartCarringObject(carriable, interactionPoint, carringType);
        }

        characterController.ReduceSpeedByPercent(movementSpeedReductionPercentWithObj[sizeDifference]);
    }

    public void StopMovingWithObject()
    {
        interactionPoint.parent = trs;
        interactionPoint.position = Vector3.zero;

        characterController.StopCarringObject();
    }
}