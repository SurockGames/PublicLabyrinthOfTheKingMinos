using SurockGames.Characters;

namespace Assets.Programming
{
    public class FollowActiveCharacter : FollowTransform
    {
        protected void OnEnable()
        {
            ActiveCharacterSetter.OnCharacterChangedEvent += ChangeCharacterToFollow;
        }

        protected void OnDisable()
        {
            ActiveCharacterSetter.OnCharacterChangedEvent -= ChangeCharacterToFollow;
        }

        private void ChangeCharacterToFollow(Character character)
        {
            targetToFollow = character.transform;
        }
    }
}