using UnityEngine;

public class ExitUnblocker : MonoBehaviour
{
    [SerializeField] private GameObject levelExit;
    [SerializeField] private DialogueActivator dialogueActivator;
    [SerializeField] private DialogueObject insufficientPointsDialogue;
    [SerializeField] private DialogueObject sufficientPointsDialogue;
    public void UnblockExit()
    {
        var gs = FindFirstObjectByType<GameSession>();
        if (gs != null && gs.CheckIfHasEnoughPoints(100))
        {
            levelExit.GetComponent<BoxCollider2D>().isTrigger = true;
            gs.SubstractScore(100);
        }
    }

    public void CheckPoints()
    {
        var gs = FindFirstObjectByType<GameSession>();
        if (gs != null && gs.CheckIfHasEnoughPoints(100))
        {
            dialogueActivator.UpdateDialogueObject(sufficientPointsDialogue);
        }
        else
        {
            dialogueActivator.UpdateDialogueObject(insufficientPointsDialogue);
        }
    }
}
