using KinematicCharacterController.Walkthrough.ClimbingLadders;
using Sirenix.OdinInspector;
using SurockGames.System;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SurockGames.Characters
{
    public class ActiveCharacterSetter : SerializedMonoBehaviour
    {
        private Dictionary<CharacterEnum, Character> characters = new Dictionary<CharacterEnum, Character>();

        public static event Action<Character> OnCharacterChangedEvent;
        private CharacterEnum activeCharacter;
        private CharacterEnum previousCharacter;

        private void Start()
        {
            InitCharacters();

            if (characters[CharacterEnum.Teseus] == null)
            {
                SetNextCharacterActive();
            }
            else
            {
                ChangeActiveCharacter();
            }
            Game.InputService.OnChangeToNextCharacterEvent += SetNextCharacterActive;
            Game.InputService.OnChangeToPreviousCharacterEvent += SetPreviousCharacterActive;
        }

        private void OnDisable()
        {
            Game.InputService.OnChangeToNextCharacterEvent -= SetNextCharacterActive;
            Game.InputService.OnChangeToPreviousCharacterEvent -= SetPreviousCharacterActive;
        }

        private void InitCharacters()
        {
            if (Player.Teseus != null)
                characters.Add(CharacterEnum.Teseus, Player.Teseus);
            else
                characters.Add(CharacterEnum.Teseus, null);

            if (Player.Minotaur != null)
                characters.Add(CharacterEnum.Minotaur, Player.Minotaur);
            else
                characters.Add(CharacterEnum.Minotaur, null);

            if (Player.Talos != null)
                characters.Add(CharacterEnum.Talos, Player.Talos);
            else
                characters.Add(CharacterEnum.Talos, null);

        }

        [Button("SetNextCharacterActive")]
        private void SetNextCharacterActive()
        {
            previousCharacter = activeCharacter;

            if (activeCharacter < CharacterEnum.NumberOfTypes - 1)
            {
                activeCharacter++;
            }
            else
            {
                activeCharacter = 0;
            }

            if (characters[activeCharacter] == null)
            {
                SetNextCharacterActive();
                return;
            }

            ChangeActiveCharacter();
        }

        [Button("SetPreviousCharacterActive")]
        private void SetPreviousCharacterActive()
        {
            previousCharacter = activeCharacter;

            if (activeCharacter > 0)
            {
                activeCharacter--;
            }
            else
            {
                activeCharacter = CharacterEnum.NumberOfTypes - 1;
            }

            if (characters[activeCharacter] == null)
            {
                SetPreviousCharacterActive();
                return;
            }

            ChangeActiveCharacter();
        }

        private void ChangeActiveCharacter()
        {
            OnCharacterChangedEvent?.Invoke(GetActiveCharacter());
        }

        private Character GetActiveCharacter()
        {
            if (characters[activeCharacter] == null)
            {
                Debug.LogError("There is no that type of character");
                return characters[previousCharacter];
            }

            return characters[activeCharacter];
        }
    }
}