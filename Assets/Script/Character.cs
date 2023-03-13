using UnityEngine;

public class Character
{
    public virtual void HandleHeal(int countOfHealPoints)
    {
        Debug.Log($"Character -> HandleHeal: {countOfHealPoints}");
    }

    public virtual void HandleDamage(int countOfHealPoints)
    {
        Debug.Log($"Character -> HandleDamage: {countOfHealPoints}");
    }
}