using UnityEngine;
using SurockGames.System;
using SurockGames.Characters;

namespace KinematicCharacterController.Walkthrough.ClimbingLadders
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Character teseusPrefab;
        [SerializeField] private Character minotaurPrefab;
        [SerializeField] private Character talosPrefab;

        public CharacterController Character;
        public static Character Teseus;
        public static Character Minotaur;
        public static Character Talos;

        private void Awake()
        {
            //InitCharacters();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            ActiveCharacterSetter.OnCharacterChangedEvent += ChangeCharacter;
        }

        private void OnDisable()
        {
            ActiveCharacterSetter.OnCharacterChangedEvent -= ChangeCharacter;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            HandleCharacterInput();
        }

        public void InitCharacters(Transform teseusSpawn, Transform minotaurSpawn, Transform talosSpawn)
        {
            if (teseusSpawn != null)
            {
                Teseus = Instantiate(teseusPrefab, teseusSpawn.position, teseusSpawn.rotation, teseusSpawn);
                Character = Teseus.GetComponent<CharacterController>();
            }
            if (minotaurSpawn != null)
            {
                Minotaur = Instantiate(minotaurPrefab, minotaurSpawn.position, minotaurSpawn.rotation, minotaurSpawn);

                if (Character == null)
                {
                    Character = Minotaur.GetComponent<CharacterController>();
                }
            }
            if (talosSpawn != null)
            {
                Talos = Instantiate(talosPrefab, talosSpawn.position, talosSpawn.rotation, talosSpawn);

                if (Character == null)
                {
                    Character = Talos.GetComponent<CharacterController>();
                }
            }

        }

        private void ChangeCharacter(Character character)
        {
            SetCharacterInputToNull();
            Character = character.GetComponent<CharacterController>();
        }

        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Build the CharacterInputs struct
            characterInputs.MoveAxisForward = Game.InputService.Axis.y;
            characterInputs.MoveAxisRight = Game.InputService.Axis.x;
            characterInputs.JumpDown = Game.InputService.IsJumping;
            characterInputs.ClimbLadder = Input.GetKeyUp(KeyCode.E);

            // Apply inputs to character
            Character.SetInputs(ref characterInputs);
        }

        private void SetCharacterInputToNull()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Build the CharacterInputs struct
            characterInputs.MoveAxisForward = 0;
            characterInputs.MoveAxisRight = 0;
            characterInputs.JumpDown = false;
            characterInputs.ClimbLadder = false;

            // Apply inputs to character
            Character.SetInputs(ref characterInputs);
        }
    }
}