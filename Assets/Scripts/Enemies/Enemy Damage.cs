
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected!");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected 2!");
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
