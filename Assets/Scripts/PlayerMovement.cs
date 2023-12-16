using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class PlayerMovement : MonoBehaviour

{

    private float horizontal;
    private float speed = 5f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private Animator anim;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }



    // Update is called once per frame

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        //Jumping!
        if (Input.GetButtonDown("Jump") && IsGrounded() == true)
        {
            anim.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // Ends Jumping anim, not a good idea
        anim.SetFloat("yVelocity", rb.velocity.y);

        if (rb.velocity.y == 0f)
        {
            anim.SetBool("IsJumping", false);
        }
        Flip();

        //Blocks movement during dialogue
        if (DialogueManager.IsActive == true)
        {
            rb.velocity = Vector2.zero;
            return;
        }
    }
    private void FixedUpdate()
    {
        //Movement !
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if ((horizontal > 0f) || (!isFacingRight && horizontal < 0f))
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    //Grounded check for jumping
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //Player Flip

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}