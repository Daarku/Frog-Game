using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("Iframes")]
    [SerializeField] private float invDuration;
    [SerializeField] private float flashNumber;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("dead");
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<Grappling>().enabled = false;

                dead = true;
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TakeDamage(1);
        }
    }

    //Iframe Counters
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < flashNumber; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(invDuration / (flashNumber * 1.5f));
            spriteRenderer.color = new Color(1, 1, 1, 1f);
            yield return new WaitForSeconds(invDuration / (flashNumber * 1.5f));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}