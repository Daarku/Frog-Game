using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float ShotCooldown;
    private Animator animator;
    private PlayerMovement playerMovement;
    private float Cooldown = Mathf.Infinity;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && ShotCooldown > Cooldown)
        {
            Attacking();
        }
       ShotCooldown = Time.deltaTime;
    }

    void Attacking()
    {
        Cooldown = 0;
        animator.SetTrigger("Attack");
    }

}
