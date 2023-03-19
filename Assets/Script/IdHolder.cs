using Game_Managers;
using Interfaces;
using UnityEngine;

public class IdHolder : MonoBehaviour, IInteractable
{
    public int Id { get; private set; }

    private void Start()
    {
        Id = InteractableGameObjectsBankOfIds.Instance.GetId();
    }

    private void OnDestroy()
    {
        InteractableGameObjectsBankOfIds.Instance.ReturnId(Id);
    }
}