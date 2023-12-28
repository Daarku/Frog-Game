using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;

    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning", true);
    }

    void Update()
    {
        Vector2 direction = (currentPoint.position - transform.position).normalized;

        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(speed, 0);
        }

        rb.velocity = direction * speed; // Update velocity based on direction

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            if (currentPoint == pointB.transform)
            {
                flip();
                currentPoint = pointA.transform;
            }
            else
            {
                flip();
                currentPoint = pointB.transform;
            }
        }
    }
    // Flipping the enemy sprite!
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // This entire section is just to see the points in the editor & how the enemy will move roughly
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);

    }
}
