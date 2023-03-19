using UnityEngine;

namespace Player
{
    public class PlayerCharacter : Character
    {
        public override void HandleHeal(int countOfHealPoints)
        {
            Debug.Log($"Player -> HandleHeal: {countOfHealPoints}");
        }

        public override void HandleDamage(int countOfHealPoints)
        {
            Debug.Log($"Player -> HandleDamage: {countOfHealPoints}");
        }
    }
}