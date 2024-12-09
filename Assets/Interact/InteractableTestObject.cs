using UnityEngine;

public class InteractableTestObject : MonoBehaviour, IInteractable
{
    public void Interact(ICharacter interactInitiator)
    {
        Debug.Log("Interacted with " + interactInitiator);
    }
}
