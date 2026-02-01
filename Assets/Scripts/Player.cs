using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get; set; }

    void Awake()
    {
        dialogueUI = FindFirstObjectByType<DialogueUI>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interactable?.Interact(this);
        }
    }
}
