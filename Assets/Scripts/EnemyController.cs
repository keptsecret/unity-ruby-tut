using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;

    public bool vertical;
    public float changeTime = 3.0f;

    float timer;
    int direction = 1;

    bool broken = true;

    Rigidbody2D rb;
    Animator animator;

    public ParticleSystem smokeEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        if (vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = rb.position;
        if (vertical)
        {
            position.y += Time.deltaTime * speed * direction;
        }
        else
        {
            position.x += Time.deltaTime * speed * direction;
        }

        rb.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController rubyController = collision.gameObject.GetComponent<RubyController>();

        if (rubyController != null)
        {
            rubyController.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rb.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }
}
