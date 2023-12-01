using UnityEngine;
using CharacterController = KinematicCharacterController.CharacterController;

[RequireComponent(typeof(CharacterController))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] private SizeEnum size;
    public SizeEnum Size => size;

    protected CharacterController controller;

    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
}
