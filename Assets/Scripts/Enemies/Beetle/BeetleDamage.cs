using UnityEngine;

public class BeetleDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float topCollisionThreshold = 0.5f; // Adjust this value as needed

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D playerCollider = collision.gameObject.GetComponent<Collider2D>();

            // Check if any point in the player's collider is above the enemy's collider
            if (IsPlayerAboveEnemy(playerCollider))
            {
                Destroy(gameObject); // Destroy the enemy if touched from the top
            }
            else
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage); // Player takes damage
            }
        }
    }

    // Check if any point in the player's collider is above the enemy's collider
    private bool IsPlayerAboveEnemy(Collider2D playerCollider)
    {
        Collider2D enemyCollider = GetComponent<Collider2D>();

        if (playerCollider == null || enemyCollider == null)
        {
            return false;
        }

        Bounds playerBounds = playerCollider.bounds;
        Bounds enemyBounds = enemyCollider.bounds;

        return playerBounds.min.y > enemyBounds.max.y - topCollisionThreshold;
    }
}
