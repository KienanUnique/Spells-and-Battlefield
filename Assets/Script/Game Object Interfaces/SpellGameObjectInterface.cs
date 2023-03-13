using System;
using UnityEngine;

[RequireComponent(typeof(InteractableGameObjectInterface))]
public class SpellGameObjectInterface : MonoBehaviour, ISpellInteractable
{
    public int Id => _interactableGameObjectInterface.Id;
    public event Action<int> HandleDamageEvent;
    public event Action<int> HandleHealEvent;
    private InteractableGameObjectInterface _interactableGameObjectInterface;

    public void HandleDamage(int countOfHealPoints)
    {
        HandleDamageEvent?.Invoke(countOfHealPoints);
    }

    public void HandleHeal(int countOfHealPoints)
    {
        HandleHealEvent?.Invoke(countOfHealPoints);
    }

    private void Awake()
    {
        _interactableGameObjectInterface = GetComponent<InteractableGameObjectInterface>();
    }
}