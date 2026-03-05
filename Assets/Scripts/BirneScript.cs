using UnityEngine;

public class BirneScript : MonoBehaviour
{
    
    [SerializeField] private DialogueActivator dialogueActivator;
    [SerializeField] private DialogueObject insufficientPointsDialogue;
    [SerializeField] private DialogueObject sufficientPointsDialogue;
    public void GiveCoins()
    {
        var gs = FindFirstObjectByType<GameSession>();
        if (gs != null && gs.CheckIfHasEnoughPoints(10))
        {
            gs.SubstractScore(10);
        }
    }

    public void CheckPoints()
    {
        var gs = FindFirstObjectByType<GameSession>();
        if (gs != null && gs.CheckIfHasEnoughPoints(10))
        {
            dialogueActivator.UpdateDialogueObject(sufficientPointsDialogue);
        }
        else
        {
            dialogueActivator.UpdateDialogueObject(insufficientPointsDialogue);
        }
    }
}
