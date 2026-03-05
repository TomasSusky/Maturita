using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameSession gs = FindFirstObjectByType<GameSession>();
            if (gs != null)
            {
                gs.SetCurrentRespawnPoint(gameObject);
            }
        }
    }
}
