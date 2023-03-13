using UnityEngine;

public class InteractableGameObjectInterface : MonoBehaviour, IInteractable
{
    [HideInInspector] public int Id => _id;
    private int _id;

    private void Start()
    {
        _id = InteractableGameObjectsBankOfIds.Instance.GetId();
    }

    private void OnDestroy()
    {
        InteractableGameObjectsBankOfIds.Instance.ReturnId(_id);
    }
}