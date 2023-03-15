using UnityEngine;

[RequireComponent(typeof(IdHolder))]
public class EnemyController : MonoBehaviour, IEnemy
{
    private IdHolder _idHolder;
    private Character _character;

    public int Id => _idHolder.Id;

    private void Awake()
    {
        _idHolder = GetComponent<IdHolder>();
        _character = new Character();
    }

    public void HandleHeal(int countOfHealthPoints)
    {
        _character.HandleHeal(countOfHealthPoints);
    }

    public void HandleDamage(int countOfHealthPoints)
    {
        _character.HandleDamage(countOfHealthPoints);
    }
}
