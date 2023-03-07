using UnityEngine;

public class PlayerCharacter
{
    public void HandleHeal(int countOfHealPoints)
    {
        Debug.Log($"Player -> HandleHeal: {countOfHealPoints}");
    }

    public void HandleDamage(int countOfHealPoints)
    {
        Debug.Log($"Player -> HandleDamage: {countOfHealPoints}");
    }
}