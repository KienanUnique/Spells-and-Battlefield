using UnityEngine;

public class EnemyController : MonoBehaviour, ICharacter
{
    public void HandleDamage(int countOfHealPoints)
    {
        Debug.Log($"Enemy -> HandleDamage: {countOfHealPoints}");
    }

    public void HandleHeal(int countOfHealPoints)
    {
        Debug.Log($"Enemy -> HandleHeal: {countOfHealPoints}");
    }

    public void HandleVelocityBoost()
    {
        Debug.Log($"Enemy -> HandleVelocityBoost");
    }
}
