using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup = 10;

    bool wasCollected = false;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;
            FindFirstObjectByType<GameSession>().AddToScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickupSFX, transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
