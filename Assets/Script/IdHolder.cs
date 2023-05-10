using System;
using Game_Managers;
using Interfaces;
using UnityEngine;

public class IdHolder : MonoBehaviour, IInteractable
{
    public int Id { get; private set; }

    public int CompareTo(object obj)
    {
        if (obj is IInteractable interactableObject)
        {
            return interactableObject.Id == Id ? 0 : 1;
        }

        throw new InvalidCastException();
    }

    private void Start()
    {
        Id = gameObject.GetInstanceID();
    }
}