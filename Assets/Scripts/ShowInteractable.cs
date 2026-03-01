using UnityEngine;
using TMPro;

public class ShowInteractable : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI interactableUI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactableUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactableUI.gameObject.SetActive(false);
        }
    }
}
